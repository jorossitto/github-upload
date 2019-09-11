using Common;
using NUnit.Framework;
using System;
using System.Reflection;

namespace CommonTests
{
    [TestFixture]
    public class InternalClassTests
    {
        [Test]
        public void Class1()
        {
            var obj1 = new Class1();
            Assert.AreEqual("a", obj1.GetString1());
            Assert.AreEqual("A", obj1.GetString1Upper());
            Assert.AreEqual("", obj1.GetString3Upper());
            Assert.AreEqual("abc", obj1.GetString0());
            Assert.AreEqual("ABC", obj1.GetString0Upper());

        }

        [Test]
        public void Class2()
        {
            var obj2 = new Class1.Class2();
            Assert.AreEqual("b", obj2.GetString2());
            Assert.AreEqual("B", obj2.GetString2Upper());
            Assert.AreEqual("", obj2.GetString3Upper());

            Assert.AreEqual("xyz", obj2.GetString0());
            Assert.AreEqual("XYZ", obj2.GetString0Upper());
        }

        [Test]
        public void Class3()
        {
            var type3 = typeof(Class1.Class2).GetNestedType("Class3", BindingFlags.NonPublic);
            var methodInfo = type3.GetMethod("GetString3", BindingFlags.NonPublic | BindingFlags.Instance);
            var obj3 = Activator.CreateInstance(type3);
            Assert.AreEqual("c", methodInfo.Invoke(obj3, null));
            Assert.AreEqual("C", obj3.GetString3Upper());
        }
    }
}
