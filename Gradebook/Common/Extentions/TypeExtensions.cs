using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class TypeExtensions
    {
        public static TypeDescription GetDescription(this Type type)
        {
            Contract.Requires(type != null);
            return new TypeDescription
            {
                AssemblyQualifiedName = type.AssemblyQualifiedName,
                FullName = type.FullName
            };

        }
    }

    public class TypeDescription
    {
        public string FullName { get; set; }
        public string AssemblyQualifiedName { get; set; }
    }
}
