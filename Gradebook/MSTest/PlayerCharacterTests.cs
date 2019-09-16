using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fundamentals;
using System.Text.RegularExpressions;
using System.Linq;

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

        [TestMethod]
        public void StartWithDefaultHealth()
        {
            var systemUnderTest = new PlayerCharacter(SpecialDefence.Null);
            Assert.AreEqual(100, systemUnderTest.Health);
        }

        [TestMethod]
        public void TakeDamage()
        {
            var systemUnderTest = new PlayerCharacter(SpecialDefence.Null);
            systemUnderTest.TakeDamage(1);
            Assert.AreEqual(99, systemUnderTest.Health);
        }

        [TestMethod]
        public void IncreaseHealthAfterSleeping()
        {
            var systemUnderTest = new PlayerCharacter(SpecialDefence.Null);
            systemUnderTest.Sleep();

            Assert.IsTrue(systemUnderTest.Health >= 101 && systemUnderTest.Health <= 200);
        }

        public void CalculateFullName()
        {
            var systemUnderTest = new PlayerCharacter(SpecialDefence.Null);
            systemUnderTest.FirstName = "Sarah";
            systemUnderTest.LastName = "Smith";

            Assert.AreEqual("Sarah Smith", systemUnderTest.FullName);

            StringAssert.StartsWith(systemUnderTest.FullName, "Sarah");
            StringAssert.EndsWith(systemUnderTest.FullName, "Smith");
            StringAssert.Contains(systemUnderTest.FullName, "ah Sm");
            StringAssert.Matches(systemUnderTest.FullName,
                new Regex("[A-Z]{1}[a-z] + [A-Z]{1}[a-z]+"));
        }

        [TestMethod]
        public void HaveALongBow()
        {
            var systemUnderTest = new PlayerCharacter(SpecialDefence.Null);
            CollectionAssert.Contains(systemUnderTest.Weapons, "Long Bow");
            CollectionAssert.DoesNotContain(systemUnderTest.Weapons, "Staff Of Wonder");
        }

        [TestMethod]
        public void HaveAllExpectedWeapons()
        {
            var systemUnderTest = new PlayerCharacter(SpecialDefence.Null);
            var expectedWeapons = new[]
            {
                "Long Bow",
                "Short Bow",
                "Short Sword"
            };

            CollectionAssert.AreEquivalent(expectedWeapons, systemUnderTest.Weapons);
        }

        [TestMethod]
        public void HaveNoDuplicateWeapons()
        {
            var systemUnderTest = new PlayerCharacter(SpecialDefence.Null);
            CollectionAssert.AllItemsAreUnique(systemUnderTest.Weapons);
        }

        [TestMethod]
        public void HaveAtLeastOneSword()
        {
            var systemUnderTest = new PlayerCharacter(SpecialDefence.Null);
            Assert.IsTrue(systemUnderTest.Weapons.Any(weapon => weapon.Contains("Sword")));
        }

        [TestMethod]
        public void HaveNoEmptyWeapons()
        {
            var systemUnderTest = new PlayerCharacter(SpecialDefence.Null);
            Assert.IsFalse(systemUnderTest.Weapons.Any(weapon => string.IsNullOrWhiteSpace(weapon)));
        }

    }
}
