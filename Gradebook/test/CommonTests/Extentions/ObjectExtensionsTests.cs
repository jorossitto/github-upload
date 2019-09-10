using ACM.BL;
using Common;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace CommonTests.Extentions
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [Test]
        public void ToJsonStringTest()
        {
            var obj1 = int.MaxValue;
            Debug.WriteLine("obj1 - " + obj1.ToJsonString());

            var obj2 = new DateTime(200, 12, 31);
            Debug.WriteLine("obj2 - " + obj2.ToJsonString());

            var obj3 = new ReferenceDataItem
            {
                Code = "xyz",
                Description = "123"
            };
            Debug.WriteLine("obj3 - " + obj3.ToJsonString());

            IEnumerable<IReferenceDataSource> obj4 = new List<IReferenceDataSource> { new SqlReferenceDataSource() };
            Debug.WriteLine("obj4 - " + obj4.ToJsonString());

        }

        [Test]
        public void ToJsonTypeDescriptionTest()
        {
            var obj1 = int.MaxValue;
            Debug.WriteLine("obj1 - " + obj1.GetJsonTypeDescription());

            var obj2 = new DateTime(200, 12, 31);
            Debug.WriteLine("obj2 - " + obj2.GetJsonTypeDescription());

            var obj3 = new ReferenceDataItem
            {
                Code = "xyz",
                Description = "123"
            };
            Debug.WriteLine("obj3 - " + obj3.GetJsonTypeDescription());

            IEnumerable<IReferenceDataSource> obj4 = new List<IReferenceDataSource> { new SqlReferenceDataSource() };
            Debug.WriteLine("obj4 - " + obj4.GetJsonTypeDescription());

        }
    }
}
