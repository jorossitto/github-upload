using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public abstract class ApiDataSource
    {
        private readonly string Name = "API";
    }

    public class ApiReferenceDataSource : ApiDataSource, IReferenceDataSource
    {
        public IEnumerable<ReferenceDataItem> GetItems()
        {
            return new List<ReferenceDataItem>
            {
                new ReferenceDataItem {Code = "xyz", Description = "From API"},
                new ReferenceDataItem {Code = "xyz", Description = "From API 2"}
            };
        }
    }
}
