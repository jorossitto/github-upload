using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fundamentals;
using System;

namespace Fundamentals.Test
{
    [TestClass]
    [TestCategory("Enemy Creation")]
    public class EnemyFactoryShould
    {
        [TestMethod]
        public void NotAllowNullName()
        {
            Console.WriteLine("Creating Enemy Factory");
            var systemUnderTest = new EnemyFactory();

            Console.WriteLine("Calling Create Method");
            Assert.ThrowsException<ArgumentNullException>(
                () => systemUnderTest.Create(null)
                );
        }

        [TestMethod]
        public void OnlyAllowKingOrQueenBossEnimies()
        {
            var systemUnderTest = new EnemyFactory();

            EnemyCreationException ex = 
            Assert.ThrowsException<EnemyCreationException>(
                () => systemUnderTest.Create("Zombie", true));

            Assert.AreEqual("Zombie", ex.RequestedEnemyName);
        }

        [TestMethod]
        public void CreateNormalEnemyByDefault()
        {
            var systemUnderTest = new EnemyFactory();

            Enemy enemy = systemUnderTest.Create("Zombie");

            Assert.IsInstanceOfType(enemy, typeof(NormalEnemy));
        }

        [TestMethod]
        public void CreateBossEnemy()
        {
            var systemUnderTest = new EnemyFactory();

            var enemy = systemUnderTest.Create("ZombieKing", true);

            Assert.IsInstanceOfType(enemy, typeof(BossEnemy));
        }

        [TestMethod]
        public void CreateSeperateInstances()
        {
            var systemUnderTest = new EnemyFactory();

            var enemy1 = systemUnderTest.Create("ZombieKing", true);
            var enemy2 = systemUnderTest.Create("ZombieKing", true);

            Assert.AreNotSame(enemy1, enemy2);
        }

    }
}
