using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Moq;
using ACM.BL;
using TestNinja.Fundamentals;

namespace ACM.Tests
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        [Test]
        public void RetrieveTest()
        {
            var orderRepository = new OrderRepository();
            var expected = new Order(10)
            {
                OrderDate = new DateTimeOffset(DateTime.Now.Year, 4, 14, 10, 00, 00,
                    new TimeSpan(7, 0, 0))
        };

            var actual = orderRepository.Retrieve(10);

            Assert.AreEqual(expected.OrderDate, actual.OrderDate);
        }
    }
}
