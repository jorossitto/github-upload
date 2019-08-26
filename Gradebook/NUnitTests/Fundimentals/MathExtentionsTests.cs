using NUnit.Framework;
using System.Linq;
using Test.Fundamentals;
using Common;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathExtentionsTests
    {
        //private MathExtentions _math;

        //Setup
        //Teardown

        [SetUp]
        public void SetUp()
        {
            //_math = new Math();
        }

        [Test]
        //[Ignore("Because I wanted to!")]
        public void Add_WhenCalled_ReturnSumOfArguments()
        {
            //Act
            var result = MathExtentions.Add(1, 2);

            //Assert
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(2,1,2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            //Act
            var result = MathExtentions.Max(a, b);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
            var result = MathExtentions.GetOddNumbers(5);

            //Assert.That(result, Is.Not.Empty);
            //Assert.That(result.Count(), Is.EqualTo(3));
            //Assert.That(result, Does.Contain(1));
            //Assert.That(result, Does.Contain(3));
            //Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));
            //Assert.That(result, Is.Ordered);
            //Assert.That(result, Is.Unique);
        }

        [Test]
        [TestCase("5000", "2000", 40f)]
        [TestCase("5000", "5000", 100f)]
        [TestCase("5000", "0", 0f)]
        public void CalculatePercentOfGoalStepsTest(string goalSteps, string actualSteps, float expectedResult)
        {

            var actual = MathExtentions.CalculatePercentOfGoalSteps(goalSteps, actualSteps);
            Assert.AreEqual(expectedResult, actual);
        }

        [Test]
        [TestCase(null, "2000")]
        [TestCase("5000", null)]
        [TestCase(null, null)]
        public void CalculatePercentOfGoalStepsTest(string goalSteps, string actualSteps)
        {

            //var actual = MathExtentions.CalculatePercentOfGoalSteps(goalSteps, actualSteps);
            Assert.That(() => MathExtentions.CalculatePercentOfGoalSteps(goalSteps, actualSteps), Throws.ArgumentException);

        }
    }
}
