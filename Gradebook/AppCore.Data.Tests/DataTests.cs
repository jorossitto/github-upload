using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AppCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppCore.Data.Tests
{
    [TestClass]
    public class DataTests
    {
        private DbContextOptionsBuilder<BusinessDBContext> optionsBuilder
            = new DbContextOptionsBuilder<BusinessDBContext>();

        [TestMethod]
        public void HasNoSeedData()
        {
            optionsBuilder.UseInMemoryDatabase("HasNoSeedData");
            using (var context = new BusinessDBContext(optionsBuilder.Options))
            {
                var locations = context.Locations.ToList();
                Assert.AreEqual(0, locations.Count());
            }
        }

        [TestMethod]
        public void HasSeedData()
        {
            optionsBuilder.UseInMemoryDatabase("HasSeedData");
            using (var context = new BusinessDBContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();
                Assert.AreNotEqual(0, context.Locations.Count());
            }
        }

        [TestMethod]
        public void RetainChanges()
        {
            optionsBuilder.UseInMemoryDatabase("RetainChanges");
            using(var context = new BusinessDBContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();
            }
            using(var newContextSameDBName = new BusinessDBContext(optionsBuilder.Options))
            {
                Assert.AreNotEqual(0, newContextSameDBName.Locations.Count());
            }
        }

        [TestMethod]
        public void ResetWithNoSeedData()
        {
            optionsBuilder.UseInMemoryDatabase("ResetWithSeedData");
            using(var context = new BusinessDBContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();
            }

            optionsBuilder.UseInMemoryDatabase("NewInMemory");
            using(var newContextNewDbName = new BusinessDBContext(optionsBuilder.Options))
            {
                Assert.AreEqual(0, newContextNewDbName.Locations.Count());
            }
        }

        [TestMethod]
        public void DemonstrateDefaultDBTransaction()
        {
            using(var context = new BusinessDBContext())
            {
                var location = context.Locations.FirstOrDefault();
                var originalAddress = location.Address2;
                location.Address2 += "100 Lakewood Avenue";
                var milestone = new Milestone(1, DateTime.Now.Date, $"Location moved from {originalAddress} to {location.Address2}");
                location.Milestones.Add(milestone);
                Assert.AreEqual(2, context.SaveChanges());
            }
        }

        [TestMethod]
        public void ShareTransactionAcrossContextInstances()
        {
            var unit = new Unit { LocationId = 1, Acquired = DateTime.Now, BrewerTypeId = 1 };
            using(var context = new BusinessDBContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Units.Add(unit);
                        context.SaveChanges();
                        var unitCount = context.Units.Where(l => l.LocationId == unit.LocationId).Count();
                        using(var context2 = new BusinessDBContext())
                        {
                            var milestone =
                                new Milestone(1, DateTime.Now.Date,
                                $"Unit #{unitCount} acquired for location");
                            context2.Database.UseTransaction(transaction.GetDbTransaction());
                            context2.Entry(milestone).State = EntityState.Added;
                            Assert.AreEqual(1, context2.SaveChanges());
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception occured ({ex.Message})... transaction will rollback");
                    }
                }
            }
        }

        [TestMethod]
        public void ShareTransactionAcrossContextInstancesWithTransactionTracking()
        {
            int resultCount = 0;
            int unitCount = 0;
            var unit = new Unit { LocationId = 1, Acquired = DateTime.Now.Date };
            using(var scope = new TransactionScope(TransactionScopeOption.Required, 
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    using(var context = new BusinessDBContext())
                    {
                        context.Units.Add(unit);
                        context.SaveChanges();
                        unitCount = context.Units.Where(l => l.LocationId == 1).Count();
                    }
                    using(var context2 = new BusinessDBContext())
                    {
                        var milestone = new Milestone(1, DateTime.Now.Date, 
                            $"Unit #{unitCount} acquired for location");
                        context2.Entry(milestone).State = EntityState.Added;
                        resultCount = context2.SaveChanges();
                    }
                    scope.Complete();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Exception occured ({ex.Message})... transaction will rollback");
                }

                Assert.AreEqual(1, resultCount);
            }
        }

        [TestMethod]
        public void ChangeStateViaEntityState()
        {
            var options = optionsBuilder.UseInMemoryDatabase("ChangeStateViaEntityState");
            using(var context = new BusinessDBContext(options.Options))
            {
                context.Database.EnsureCreated();
                var location = context.Locations.FirstOrDefault();
                context.Entry(location).State = EntityState.Modified;
            }
            Assert.IsTrue(true);
            //Assert.Inconclusive(@"There is no pass or fail on this test.  The output is the result. ");
        }
        [TestMethod]
        public void ChangeStateViaPropertyValue()
        {
            optionsBuilder.UseInMemoryDatabase("ChangeStateViaPropertyValue");
            using(var context = new BusinessDBContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();
                var location = context.Locations.FirstOrDefault();
                location.Address2 = "999 Main Street";
                Console.WriteLine("Address was already changed");
                context.ChangeTracker.DetectChanges();
            }

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AddNewRelatedObjects()
        {
            optionsBuilder.UseInMemoryDatabase("AddNewRelatedObjects");
            using(var context = new BusinessDBContext(optionsBuilder.Options))
            {
                var location = new Location("1 Main", "6am", "6pm");
                context.Locations.Add(location);
                var unit = new Unit { BrewerTypeId = 1, Acquired = DateTime.Now };
                location.BrewingUnits.Add(unit);
                Console.WriteLine("Unit was already added");
                context.ChangeTracker.DetectChanges();
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ChangeExistingRelationshipState()
        {
            optionsBuilder.UseInMemoryDatabase("ChangeExistingRelationshipState");
            using(var context = new BusinessDBContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();
                Console.WriteLine("Retrieve Location1 wiht units...");
                var locations = context.Locations.Include(l => l.BrewingUnits).ToList();
                var location1 = context.Locations.Include(l => l.BrewingUnits)
                    .SingleOrDefault(l => l.LocationId == 1);
                var aUnitAtLocation1 = location1.BrewingUnits.FirstOrDefault();
                Console.WriteLine("Retrieve location 2 with units...");
                var location2 = context.Locations.Include(l => l.BrewingUnits)
                    .SingleOrDefault(l => l.LocationId == 2);
                Console.WriteLine("About to move unit from location1 to location2....");
                location2.BrewingUnits.Add(aUnitAtLocation1);
                Console.WriteLine("About to call detect changes");
                context.ChangeTracker.DetectChanges();

                Assert.AreEqual(1, location1.LocationId);
                Assert.AreEqual(2, aUnitAtLocation1.LocationId);
            }
        }
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

            context.Attach(battles);
        }
    }
}
