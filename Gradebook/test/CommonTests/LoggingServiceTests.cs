using NUnit.Framework;
using ACM.BL;
using System.Collections.Generic;
using AppCore.Data;
using Common;

//Todo Merge using TestNinja.Mocking;

namespace CommonTests
{
    public class LoggingServiceTests
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void WriteToFileTest()
        {
            var changedItems = new List<ILoggable>();
            var customer = Customer.CreateDefaultCustomer();
            changedItems.Add(customer);
            var product = Product.DefaultTestProduct();
            changedItems.Add(product);

            LoggingService.WriteToFile(changedItems);

            //Nothing to assert
        }
    }
}