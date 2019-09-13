using Common;
using NUnit.Framework;
using System;
using System.Xml;

namespace CommonTests
{
    [TestFixture]
    public class DateTimeExtentionsTests
    {
        [Test]
        public void ToXmlDateTime()
        {
            // Arrange
            var dateTime = new DateTime(2013, 10, 24, 13, 10, 15, 951);
            Assert.AreEqual("2013-10-24T13:10:15.951Z", dateTime.ToXmlDateTime());
            Assert.AreEqual("2013-10-24T13:10:15.951-04:00", 
                dateTime.ToXmlDateTime(XmlDateTimeSerializationMode.Local));

        }
    }
}
