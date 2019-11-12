using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.Domain;
using Microsoft.EntityFrameworkCore;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppCore.Data.Tests
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        public void QueryWithFunction()
        {
            var inMemoryOptions = new DbContextOptionsBuilder<BusinessDBContext>()
                .UseInMemoryDatabase("QueryWithFunction").Options;
            var context = new BusinessDBContext(inMemoryOptions);
            SeedData(context);
            var battles = context.Battles.Select(
                b => new
                {
                    b.Name,
                    Days = BusinessDBContext.DaysInBattle(b.StartDate, b.EndDate)
                })
                .ToList();

            Assert.AreNotEqual(0, battles.Count());
                

        }

        private void SeedData(BusinessDBContext context)
        {
            var battles = new List<Battle>
            {
                        new Battle
                        {
                            Name = "Battle of Okehazama",
                            StartDate = new DateTime(1560, 05, 01),
                            EndDate = new DateTime(1560, 06, 15)
                        },

                        new Battle
                        {
                            Name = "Battle of Shiroyama",
                            StartDate = new DateTime(1877, 09, 24),
                            EndDate = new DateTime(1877, 09, 24)
                        },

                        new Battle
                        {
                            Name = "Siege of Osaka",
                            StartDate = new DateTime(1614, 01, 01),
                            EndDate = new DateTime(1615, 12, 31)
                        },

                        new Battle
                        {
                            Name = "Boshin War",
                            StartDate = new DateTime(1868, 01, 01),
                            EndDate = new DateTime(1869, 01, 01)
                        }
            };
        }
    }
}
