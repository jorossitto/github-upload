using ACM.BL;
using Common;
using Moq;
using NUnit.Framework;
using System;

namespace CommonTests
{
    [TestFixture]
    public class OrderControllerTests
    {

        [Test]
        public void PlaceOrder_GoodValues_EverythingWorks()
        {
            // Arrange
            var orderController = new OrderController();

            //Console.WriteLine("Hello World");
            Console.WriteLine(orderController.customerRepository);

            var customer = new Customer() { EmailAddress = "Test"};
            Order order = new Order();
            Payment payment = new Payment() { PaymentType = 1 };
            bool allowSplitOrders = true;
            bool emailReceipt = true;

            Console.WriteLine(orderController.customerRepository);
            // Act
            OperationResult result = orderController.PlaceOrder(
                customer,
                order,
                payment,
                allowSplitOrders,
                emailReceipt);

            // Assert
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(0, result.MessageList.Count);
        }

        [Test]
        public void PlaceOrder_CustomerisNull_ThrowsError()
        {
            // Arrange
            var orderController = new OrderController(); 
            Customer customer = null;
            Order order = new Order();
            Payment payment = new Payment() { PaymentType = 1 };
            bool allowSplitOrders = true;
            bool emailReceipt = true;

            // Assert
            Assert.That(() => orderController.PlaceOrder(
                customer,
                order,
                payment,
                allowSplitOrders,
                emailReceipt), Throws.ArgumentNullException);
        }

        [Test]
        public void PlaceOrder_EmailisBlank_ThrowsError()
        {
            // Arrange
            var orderController = new OrderController();

            //Console.WriteLine("Hello World");
            Console.WriteLine(orderController.customerRepository);

            var customer = new Customer() { EmailAddress = "" };
            Order order = new Order();
            Payment payment = new Payment() { PaymentType = 1 };
            bool allowSplitOrders = true;
            bool emailReceipt = true;

            Console.WriteLine(orderController.customerRepository);
            // Act
            OperationResult result = orderController.PlaceOrder(
                customer,
                order,
                payment,
                allowSplitOrders,
                emailReceipt);

            // Assert
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(1, result.MessageList.Count);
        }
    }
}
