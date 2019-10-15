using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public static class IReferenceDataSourceExtensions
    {
        public static IEnumerable<ReferenceDataItem> GetItemsByCode
            (this IReferenceDataSource source, string code)
        {
            Contract.Requires(source != null);
            return source.GetItems().Where(x => x.Code == code);
        }
    }
}
