using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
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
