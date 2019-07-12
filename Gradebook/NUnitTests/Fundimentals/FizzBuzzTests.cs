using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        [TestCase(1,"1")]
        [TestCase(3, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        public void GetOutput_WhenCalled_ReturnAnswer(int a, string expectedResult)
        {
            //Act
            var result = FizzBuzz.GetOutput(a);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}

