using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ObjectExtensions
    {
        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string GetJsonTypeDescription(this object obj)
        {
            Contract.Requires(obj != null);

            var description = obj.GetType().GetDescription();
            return description.ToJsonString();

        }
    }
}
