using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fundamentals;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Fundamentals.Test
{
    public static class CustomAsserts
    {
        public static void IsInRange(this Assert assert, int actual, int expectedMinValue,
            int expectedMaxValue)
        {
            if(actual < expectedMinValue || actual > expectedMaxValue)
            {
                throw new AssertFailedException($"{ actual } was not between " +
                    $"{expectedMinValue} and {expectedMaxValue} expected range ");
            }
        }

        public static void AllItemsNotNullOrWhitespace
            (this CollectionAssert collectionAssert, ICollection<string> collection)
        {
            foreach (var item in collection)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    throw new AssertFailedException
                        ("One or more items are null or white space");
                }
            }
            
        }

        public static void AllItemsSatisfy<T>(this CollectionAssert collectionAssert,
            ICollection<T> collection, Predicate<T> predicate)
        {
            foreach (var item in collection)
            {
                if(!predicate(item))
                {
                    throw new AssertFailedException("All items do not satisfy predicate");
                }
            }
        }

        public static void All<T>(this CollectionAssert collectionAssert,
            ICollection<T> collection, Action<T> assert)
        {
            foreach (var item in collection)
            {
                assert(item);
            }
        }

        public static void NotNullOrWhiteSpace(this StringAssert stringAssert, string actual)
        {
            if(string.IsNullOrWhiteSpace(actual))
            {
                throw new AssertFailedException($"Value:{actual} is null or white space");
            }
        }

        public static void AtLeastOneItemSatisfies<T>(this CollectionAssert collectionAssert,
            ICollection<T> collection, Predicate<T> predicate)
        {
            foreach (var item in collection)
            {
                if(predicate(item))
                {
                    return;
                }
            }

            throw new AssertFailedException("No item satisfied predicate");
        }
    }
}
