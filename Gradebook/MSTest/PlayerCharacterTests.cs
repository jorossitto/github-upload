using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fundamentals;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace Fundamentals.Test
{
    [TestClass]
    public class PlayerCharacterTests
    {
        PlayerCharacter systemUnderTest;
        [TestInitialize]
        public void TestInitaialize()
        {
            systemUnderTest = new PlayerCharacter(SpecialDefence.Null);
            systemUnderTest.FirstName = "Sarah";
            systemUnderTest.LastName = "Smith";

        }

        [TestMethod]
        [PlayerDefaults]
        public void BeInexperiancedWhenNew()
        {

            Assert.IsTrue((bool)systemUnderTest.IsNoob);
        }

        [TestMethod]
        [PlayerDefaults]
        public void NotHaveNickNameByDefault()
        {

            Assert.IsNull(systemUnderTest.Nickname);
        }

        [TestMethod]
        [PlayerDefaults]
        public void StartWithDefaultHealth()
        {
            Assert.AreEqual(100, systemUnderTest.Health);
        }

        public static IEnumerable<object[]> Damages
        {
            get
            {
                return new List<object[]>
                {
                    new object[] { 1, 99 },
                    new object[] { 50, 50 },
                    new object[] { 100, 1 },
                    new object[] { 0, 100 },
                    new object[] { 101, 1 }
                };
            }
        }

        [DataTestMethod]
        //[DynamicData(nameof(Damages))]
        //[DataRow(1,99)]
        [CsvDataSource("Damage.csv")]
        [PlayerHealth]
        public void TakeDamage(int damage, int expectedHealth)
        {

            systemUnderTest.TakeDamage(damage);
            Assert.AreEqual(expectedHealth, systemUnderTest.Health);
        }

        [TestMethod]
        [PlayerHealth]
        public void IncreaseHealthAfterSleeping()
        {
            systemUnderTest.Sleep();
            Assert.That.IsInRange(systemUnderTest.Health, 101, 200);
        }

        [TestMethod]
        public void CalculateFullName()
        {
            Assert.AreEqual("Sarah Smith", systemUnderTest.FullName);

            StringAssert.StartsWith(systemUnderTest.FullName, "Sarah");
            StringAssert.EndsWith(systemUnderTest.FullName, "Smith");
            StringAssert.Contains(systemUnderTest.FullName, "ah Sm");
            StringAssert.Matches(systemUnderTest.FullName,
               new Regex("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
        }

        [TestMethod]
        public void HaveALongBow()
        {

            CollectionAssert.Contains(systemUnderTest.Weapons, "Long Bow");
            CollectionAssert.DoesNotContain(systemUnderTest.Weapons, "Staff Of Wonder");
        }

        [TestMethod]
        public void HaveAllExpectedWeapons()
        {

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

            CollectionAssert.AllItemsAreUnique(systemUnderTest.Weapons);
        }

        [TestMethod]
        public void HaveAtLeastOneSword()
        {
            CollectionAssert.That.AtLeastOneItemSatisfies(systemUnderTest.Weapons,
                weapon => weapon.Contains("Sword"));
            //Assert.IsTrue(systemUnderTest.Weapons.Any(weapon => weapon.Contains("Sword")));
        }

        [TestMethod]
        public void HaveNoEmptyWeapons()
        {
            CollectionAssert.That.AllItemsNotNullOrWhitespace(systemUnderTest.Weapons);
            //Assert.IsFalse(systemUnderTest.Weapons.Any(weapon => string.IsNullOrWhiteSpace(weapon)));
            CollectionAssert.That.AllItemsSatisfy(systemUnderTest.Weapons,
                weapon => !string.IsNullOrWhiteSpace(weapon));

            CollectionAssert.That.All(systemUnderTest.Weapons, weapon =>
            {
                StringAssert.That.NotNullOrWhiteSpace(weapon);
                Assert.IsTrue(weapon.Length > 5);
            });
        }

    }
}
