// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

#include "precomp.h"

LONG    ALLOC_CACHE_HANDLER::sm_nFillPattern = 0xACA50000;
HANDLE  ALLOC_CACHE_HANDLER::sm_hHeap;

//
// This class is used to implement the free list.  We cast the free'd
// memory block to a FREE_LIST_HEADER*.  The signature is used to guard against
// double deletion.  We also fill memory with a pattern.
//
class FREE_LIST_HEADER
{
public:
    SLIST_ENTRY     ListEntry;
    DWORD           dwSignature;

    enum
    {
        FREE_SIGNATURE = (('A') | ('C' << 8) | ('a' << 16) | (('$' << 24) | 0x80)),
    };
};

ALLOC_CACHE_HANDLER::ALLOC_CACHE_HANDLER(
    VOID
) : m_nThreshold(0),
    m_cbSize(0),
    m_pFreeLists(NULL),
    m_nTotal(0)
{
}

ALLOC_CACHE_HANDLER::~ALLOC_CACHE_HANDLER(
    VOID
)
{
    if (m_pFreeLists != NULL)
    {
        CleanupLookaside();
        m_pFreeLists->Dispose();
        m_pFreeLists = NULL;
    }
}

HRESULT
ALLOC_CACHE_HANDLER::Initialize(
    DWORD       cbSize,
    LONG        nThreshold
)
{
    HRESULT hr = S_OK;

    m_nThreshold = nThreshold;
    if ( m_nThreshold > 0xffff)
    {
        //
        // This will be compared against QueryDepthSList return value (USHORT).
        //
        m_nThreshold = 0xffff;
    }

    if ( IsPageheapEnabled() )
    {
        //
        // Disable acache.
        //
        m_nThreshold = 0;
    }

    //
    // Make sure the block is big enough to hold a FREE_LIST_HEADER.
    //
    m_cbSize = cbSize;
    m_cbSize = max(m_cbSize, sizeof(FREE_LIST_HEADER));
    
    //
    // Round up the block size to a multiple of the size of a LONG (for
    // the fill pattern in Free()).
    //
    m_cbSize = (m_cbSize + sizeof(LONG) - 1) & ~(sizeof(LONG) - 1);

#if defined(_MSC_VER) && _MSC_VER >= 1600 // VC10
    auto Init = [] (SLIST_HEADER* pHead)
    {
        InitializeSListHead(pHead);
    };
#else
    class Functor
    {
    public:
        void operator()(SLIST_HEADER* pHead)
        {
            InitializeSListHead(pHead);
        }
    } Init;
#endif
    
    hr = PER_CPU<SLIST_HEADER>::Create(Init,
                                       &m_pFreeLists );
    if (FAILED(hr))
    {
        goto Finished;
    }

    m_nFillPattern = InterlockedIncrement(&sm_nFillPattern);

Finished:

    return hr;
}

// static
HRESULT
ALLOC_CACHE_HANDLER::StaticInitialize(
    VOID
)
{
    //
    // Since the memory allocated is fixed size,
    // a heap is not really needed, allocations can be done
    // using VirtualAllocEx[Numa]. For now use Windows Heap.
    //
    // Be aware that creating one private heap consumes more
    // virtual address space for the worker process.
    //
    sm_hHeap = GetProcessHeap();
    return S_OK;
}


// static
VOID
ALLOC_CACHE_HANDLER::StaticTerminate(
    VOID
)
{
    sm_hHeap = NULL;
}

VOID
ALLOC_CACHE_HANDLER::CleanupLookaside(
    VOID
)
/*++
  Description:
    This function cleans up the lookaside list by removing storage space.

  Arguments:
    None.

  Returns:
     None
--*/
{
    //
    // Free up all the entries in the list.
    // Don't use InterlockedFlushSList, in order to work
    // memory must be 16 bytes aligned and currently it is 64.
    //

#if defined(_MSC_VER) && _MSC_VER >= 1600 // VC10
    auto Predicate = [=] (SLIST_HEADER * pListHeader)
    {
        PSLIST_ENTRY pl;
        LONG NodesToDelete = QueryDepthSList( pListHeader );

        pl = InterlockedPopEntrySList( pListHeader );
        while ( pl != NULL && --NodesToDelete >= 0 )
        {
            InterlockedDecrement( &m_nTotal);

            ::HeapFree( sm_hHeap, 0, pl );

            pl = InterlockedPopEntrySList(pListHeader);
        }
    };
#else
    class Functor
    {
    public:
        explicit Functor(ALLOC_CACHE_HANDLER * pThis) : _pThis(pThis)
        {
        }
        void operator()(SLIST_HEADER * pListHeader)
        {
            PSLIST_ENTRY pl;
            LONG NodesToDelete = QueryDepthSList( pListHeader );

            pl = InterlockedPopEntrySList( pListHeader );
            while ( pl != NULL && --NodesToDelete >= 0 )
            {
                InterlockedDecrement( &_pThis->m_nTotal);

                ::HeapFree( sm_hHeap, 0, pl );

                pl = InterlockedPopEntrySList(pListHeader);
            }
        }
    private:
        ALLOC_CACHE_HANDLER * _pThis;
    } Predicate(this);
#endif

    m_pFreeLists ->ForEach(Predicate);
}

LPVOID
ALLOC_CACHE_HANDLER::Alloc(
    VOID
)
{
    LPVOID pMemory = NULL;

    if ( m_nThreshold > 0 )
    {
        SLIST_HEADER * pListHeader = m_pFreeLists ->GetLocal();
        pMemory = (LPVOID) InterlockedPopEntrySList(pListHeader);  // get the real object

        if (pMemory != NULL)
        {
            FREE_LIST_HEADER* pfl = (FREE_LIST_HEADER*) pMemory;
            //
            // If the signature is wrong then somebody's been scribbling
            // on memory that they've freed.
            //
            DBG_ASSERT(pfl->dwSignature == FREE_LIST_HEADER::FREE_SIGNATURE);
        }
    }

    if ( pMemory == NULL )
    {
        //
        // No free entry. Need to alloc a new object.
        //
        pMemory = (LPVOID) ::HeapAlloc( sm_hHeap,
                                        0,
                                        m_cbSize );

        if ( pMemory != NULL )
        {
            //
            // Update counters.
            //
            m_nTotal++;
        }
    }

    if ( pMemory == NULL )
    {
        SetLastError( ERROR_NOT_ENOUGH_MEMORY );
    }
    else
    {
        FREE_LIST_HEADER* pfl = (FREE_LIST_HEADER*) pMemory;
        pfl->dwSignature = 0; // clear; just in case caller never overwrites
    }

    return pMemory;
}

VOID
ALLOC_CACHE_HANDLER::Free(
    __in LPVOID pMemory
)
{
    //
    // Assume that this is allocated using the Alloc() function.
    //
    DBG_ASSERT(NULL != pMemory);

    //
    // Use a signature to check against double deletions.
    //
    FREE_LIST_HEADER* pfl = (FREE_LIST_HEADER*) pMemory;
    DBG_ASSERT(pfl->dwSignature != FREE_LIST_HEADER::FREE_SIGNATURE);

    //
    // Start filling the space beyond the portion overlaid by the initial
    // FREE_LIST_HEADER.  Fill at most 6 DWORDS.
    //
    LONG* pl = (LONG*) (pfl+1);

    for (LONG cb = (LONG)min(6 * sizeof(LONG),m_cbSize) - sizeof(FREE_LIST_HEADER);
         cb > 0;
         cb -= sizeof(LONG))
    {
        *pl++ = m_nFillPattern;
    }

    //
    // Now, set the signature.
    //
    pfl->dwSignature = FREE_LIST_HEADER::FREE_SIGNATURE;

    //
    // Store the items in the alloc cache.
    //
    SLIST_HEADER * pListHeader = m_pFreeLists ->GetLocal();

    if ( QueryDepthSList(pListHeader) >= m_nThreshold )
    {
        //
        // Threshold for free entries is exceeded. Free the object to
        // process pool.
        //
        ::HeapFree( sm_hHeap, 0, pMemory );
    }
    else
    {
        //
        // Store the given pointer in the single linear list
        //
        InterlockedPushEntrySList(pListHeader, &pfl->ListEntry);
    }
}

DWORD
ALLOC_CACHE_HANDLER::QueryDepthForAllSLists(
    VOID
)
/*++

Description:
    
    Aggregates the total count of elements in all lists.
    
Arguments:
    
    None.

Return Value:

    Total count (snapshot).

--*/
{
    DWORD Count = 0;

    if (m_pFreeLists  != NULL)
    {
#if defined(_MSC_VER) && _MSC_VER >= 1600 // VC10
        auto Predicate = [&Count] (SLIST_HEADER * pListHeader)
        {
            Count += QueryDepthSList(pListHeader);
        };
#else
        class Functor
        {
        public:
            explicit Functor(DWORD& Count) : _Count(Count)
            {
            }
            void operator()(SLIST_HEADER * pListHeader)
            {
                _Count += QueryDepthSList(pListHeader);
            }
        private:
            DWORD& _Count;
        } Predicate(Count);
#endif
        //
        // [&Count] means that the method can modify local variable Count.
        //
        m_pFreeLists ->ForEach(Predicate);
    }

    return Count;
}

// static
BOOL
ALLOC_CACHE_HANDLER::IsPageheapEnabled(
    VOID
)
{
    BOOL        fRet = FALSE;
    BOOL        fLockedHeap = FALSE;
    HMODULE     hModule = NULL;
    HANDLE      hHeap = NULL;
    PROCESS_HEAP_ENTRY heapEntry = {0};

    //
    // If verifier.dll is loaded - we are running under app verifier == pageheap is enabled
    //
    hModule = GetModuleHandle( L"verifier.dll" );
    if ( hModule != NULL )
    {
        hModule = NULL;
        fRet = TRUE;
        goto Finished;
    }

    //
    // Create a heap for calling heapwalk 
    // otherwise HeapWalk turns off lookasides for a useful heap
    //
    hHeap = ::HeapCreate( 0, 0, 0 );
    if ( hHeap == NULL )
    {
        fRet = FALSE;
        goto Finished;
    }
    
    fRet = ::HeapLock( hHeap );
    if ( !fRet )
    {
        goto Finished;
    }
    fLockedHeap = TRUE;

    //
    // If HeapWalk is unsupported -> then running page heap
    //
    fRet = ::HeapWalk( hHeap, &heapEntry );
    if ( !fRet )
    {
        if ( GetLastError() == ERROR_INVALID_FUNCTION )
        {
            fRet = TRUE;
            goto Finished;
        }
    }

    fRet = FALSE;

Finished:

    if ( fLockedHeap )
    {
        fLockedHeap = FALSE;
        DBG_REQUIRE( ::HeapUnlock( hHeap ) );
    }

    if ( hHeap )
    {
        DBG_REQUIRE( ::HeapDestroy( hHeap ) );
        hHeap = NULL;
    }

    return fRet;
}