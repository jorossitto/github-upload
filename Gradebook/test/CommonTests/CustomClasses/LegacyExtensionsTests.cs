using ACM.BL;
using Moq;
using NUnit.Framework;
using System;

namespace CommonTests.CustomClasses
{
    [TestFixture]
    public class LegacyExtensionsTests
    {

        [Test]
        [TestCase("0201231", "12/31/1920")]
        [TestCase("1131031", "10/31/2013")]
        public void ToLegacyFormat_C20(string Arrange, DateTime Result)
        {
            Assert.AreEqual(Arrange, Result.ToLegacyFormat());
        }

        [Test]
        [TestCase("ROSSITTO, JOE", "Joe Rossitto")]
        public void ToLegacyName(string Output, string input)
        {
            Assert.AreEqual(Output, input.ToLegacyFormat());
        }
    }
}
