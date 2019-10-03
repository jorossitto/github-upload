using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public interface IReferenceDataSource
    {
        IEnumerable<ReferenceDataItem> GetItems();
    }
}
