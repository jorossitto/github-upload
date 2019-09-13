using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fundamentals;

namespace Fundamentals.Test
{
    [TestClass]
    public class PlayerCharacterTests
    {
        [TestMethod]
        public void BeInexperiancedWhenNew()
        {
            var systemUnderTest = new PlayerCharacter(SpecialDefence.Null);
            Assert.IsTrue((bool)systemUnderTest.IsNoob);
        }

        [TestMethod]
        public void NotHaveNickNameByDefault()
        {
            var systemUnderTest = new PlayerCharacter(SpecialDefence.Null);
            Assert.IsNull(systemUnderTest.Nickname);
        }
    }
}
