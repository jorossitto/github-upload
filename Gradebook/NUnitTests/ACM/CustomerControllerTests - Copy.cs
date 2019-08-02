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

            var controller = new CustomerController();

            var result = controller.GetCustomer(0);

            //Is Type of not found
            Assert.That(result, Is.TypeOf<NotFound>());

            //Not found or one of its derivities
            //Assert.That(result, Is.InstanceOf<NotFound>());

        }
        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {
            var controller = new CustomerController();

            var result = controller.GetCustomer(1);

            //Is Type of ok
            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
