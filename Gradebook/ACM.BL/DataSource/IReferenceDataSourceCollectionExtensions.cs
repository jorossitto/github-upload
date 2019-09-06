using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public static class IReferenceDataSourceCollectionExtensions
    {
        public static IEnumerable<ReferenceDataItem> GetAllItemsByCode(this IEnumerable sources, string code)
        {
            var items = new List<ReferenceDataItem>();
            foreach (var source in sources)
            {
                if (source is IReferenceDataSource refDataSource)
                {
                    items.AddRange(refDataSource.GetItemsByCode(code));
                }
            }
            return items;
        }

        public static IEnumerable<ReferenceDataItem> GetAllItemsByCode
            (this IEnumerable<IReferenceDataSource> sources, string code)
        {
            return sources.SelectMany(x => x.GetItemsByCode(code));
        }
    }
}
