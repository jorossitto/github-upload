using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {

        private DemeritPointsCalculator _DemeritPointsCalculator;

        //Setup
        //Teardown

        [SetUp]
        public void SetUp()
        {
            _DemeritPointsCalculator = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(64, 0)]
        [TestCase(0, 0)]
        [TestCase(65, 0)]
        [TestCase(66, 0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        public void CalculateDemeritPoints_SpeedIsInRange_ReturnDemeritPoints(int a, int expectedResult)
        {
            //Act
            var result = _DemeritPointsCalculator.CalculateDemeritPoints(a);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        [Test]
        [TestCase(301)]
        [TestCase(-1)]
        public void CalculateDemeritPoints_SpeedIsOutOfRange_ThrowArgumentOutofRangeException(int speed)
        {
            //Act
            //var result = _DemeritPointsCalculator.CalculateDemeritPoints(error);

            //Assert
            Assert.That(() => _DemeritPointsCalculator.CalculateDemeritPoints(speed), 
                Throws.Exception.TypeOf<ArgumentOutOfRangeException>());

        }
    }
}

