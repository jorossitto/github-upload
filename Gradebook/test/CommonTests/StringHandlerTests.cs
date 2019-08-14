using NUnit.Framework;
using Common;

namespace CommonTests
{
    public class StringHandlerTests
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void InsertSpacesTestValid()
        {
            string source = "SonicScrewdriver";
            string expected = "Sonic Screwdriver";
            var actual = source.InsertSpaces();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InsertSpacesTestWithExistingSpace()
        {
            string source = "Sonic Screwdriver";
            string expected = "Sonic Screwdriver";
            var actual = source.InsertSpaces();
            Assert.AreEqual(expected, actual);
        }
    }
}