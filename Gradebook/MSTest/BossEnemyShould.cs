using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fundamentals;

namespace Fundamentals.Test
{
    [TestClass]
    public class BossEnemyShould
    {
        [TestMethod]
        public void HaveCorrectSpecialAttackPower()
        {
            var systemUnderTest = new BossEnemy();

            Assert.AreEqual(166.6, systemUnderTest.SpecialAttackPower, .07);
        }

    }
}
