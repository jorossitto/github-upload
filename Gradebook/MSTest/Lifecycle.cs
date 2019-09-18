using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fundamentals;
using System.Text.RegularExpressions;
using System.Linq;
using System;

namespace Fundamentals.Test
{
    [TestClass]
    public class LifeCycle
    {
        static string SomeTestContext;



        [TestCleanup]
        public void LifecycleClean()
        {
            Console.WriteLine("Test Cleanup");
        }

        [ClassInitialize]
        public static void LifeCycleClassInit(TestContext context)
        {
            Console.WriteLine("Class Initialize Lifecycle");

            Console.WriteLine("data loaded from disk or some expensive object creation");
            SomeTestContext = "42";
        }

        [ClassCleanup]
        public static void LifecycleClassClean()
        {
            Console.WriteLine("ClassCleanup Lifecycle");
        }

        [TestInitialize]
        public void testA()
        {
            Console.WriteLine("Test A Start");
            Console.WriteLine($"Sharted test context: {SomeTestContext}");
        }

        [TestMethod]
        public void TestB()
        {
            Console.WriteLine("Test B Start");
            Console.WriteLine($"Sharted test context: {SomeTestContext}");
        }

    }
}
