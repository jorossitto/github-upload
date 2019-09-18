using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fundamentals;
using System.Text.RegularExpressions;
using System.Linq;
using System;

namespace Fundamentals.Test
{
    [TestClass]
    public class Assembly
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Console.WriteLine("AssemblyInit");
        }

        [AssemblyCleanup]
        public static void AssemblyClean()
        {
            Console.WriteLine("AssemblyClean");
        }

    }
}
