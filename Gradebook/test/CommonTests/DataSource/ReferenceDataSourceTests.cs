using ACM.BL;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CommonTests.DataSource
{
    [TestFixture]
    public class ReferenceDataSourceTest
    {
        [Test]
        public void GetItems()
        {
            IReferenceDataSource source;
            source = new SqlReferenceDataSource();
            Assert.AreEqual(2, source.GetItems().Count());

            source = new XmlReferenceDataSource();
            Assert.AreEqual(2, source.GetItems().Count());

            source = new ApiReferenceDataSource();
            Assert.AreEqual(2, source.GetItems().Count());

        }

        [Test]
        public void GetItemsByCode()
        {
            IReferenceDataSource source;
            source = new SqlReferenceDataSource();
            Assert.AreEqual(2, source.GetItemsByCode("xyz").Count());
        }

        [Test]
        public void GetAllItemsByCode_Array()
        {
            var sources = new IReferenceDataSource[]
            {
                new SqlReferenceDataSource(),
                new XmlReferenceDataSource(),
                new ApiReferenceDataSource()
            };

            var items = sources.GetAllItemsByCode("xyz");
            Assert.AreEqual(6, items.Count());
        }

        [Test]
        public void GetAllItemsByCode_ArrayList()
        {
            var sources = new ArrayList()
            {
                new SqlReferenceDataSource(),
                new XmlReferenceDataSource(),
                new ApiReferenceDataSource()
            };
            sources.Add("i am not a reference data source");

            var items = sources.GetAllItemsByCode("xyz");
            Assert.AreEqual(6, items.Count());
        }

        [Test]
        public void GetAllItemsByCode_IEnumerable()
        {
            var sources = new List<IReferenceDataSource>
            {
                new SqlReferenceDataSource(),
                new XmlReferenceDataSource(),
                new ApiReferenceDataSource()
            };

            var items = sources.GetAllItemsByCode("xyz");
            Assert.AreEqual(6, items.Count());
        }
    }
}
