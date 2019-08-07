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
    public class CustomerTest
    {
        [Test]
        public void FullNameTestValid()
        {
            Customer customer = new Customer();
            customer.FirstName = "Bilbo";
            customer.LastName = "Baggins";

            string expected = "Baggins, Bilbo";

            string actual = customer.FullName;

            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void FullNameFirstNameEmpty()
        {
            Customer customer = new Customer();
            customer.LastName = "Baggins";

            string expected = "Baggins";

            string actual = customer.FullName;

            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void FullNameLastNameEmpty()
        {
            Customer customer = new Customer();
            customer.FirstName = "Bilbo";

            string expected = "Bilbo";

            string actual = customer.FullName;

            Assert.AreEqual(expected, actual);

        }
        [Test]
        [TestCase("test", "Test", true)]
        [TestCase(null, "Test", false)]
        [TestCase("test", null, false)]
        [TestCase(null, null, false)]
        public void ValidateValid(string lastname, string email, bool passOrFail)
        {
            var customer = new Customer
            {
                LastName = lastname,
                EmailAddress = email
            };

            var expected = passOrFail;

            var actual = customer.Validate();

            Assert.AreEqual(expected, actual);
        }
    }
}
