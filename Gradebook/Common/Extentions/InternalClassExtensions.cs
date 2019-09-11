using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common
{
    internal static class InternalClassExtentions
    {
        public static string GetString0Upper(this Class0 obj)
        {
            return obj.GetString0().ToUpper();
        }

        public static string GetString1Upper(this Class1 obj)
        {
            return obj.GetString1().ToUpper();
        }

        public static string GetString2Upper(this Class1.Class2 obj)
        {
            return obj.GetString2().ToUpper();
        }

        public static string GetString3Upper(this object obj)
        {
            var upper = string.Empty;
            var type3 = typeof(Class1.Class2).GetNestedType("Class3", BindingFlags.NonPublic);
            if(obj.GetType() == type3)
            {
                var method = type3.GetMethod("GetString3", BindingFlags.NonPublic | BindingFlags.Instance);
                var string3 = method.Invoke(obj, null) as string;
                upper = string3.ToUpper();
            }
            return upper;
        }
    }
}
