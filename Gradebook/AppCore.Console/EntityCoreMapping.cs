using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.Data;
using AppCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace AppCore.ConsoleUI
{
    class EntityCoreMapping
    {
        public static BusinessDBContext businessDbContext = new BusinessDBContext();

        #region Methods
        public static void MainMethod()
        {
            //AddChildToExistingObjectWhileTracked();
            //InsertingDataInManyToManyRelationships();
            //GetsamuraiWithBattles();
            //RemoveJoinBetweenSamuraiAndBattleSimple();
            //RemoveBattleFromSamurai();
            //RemoveBattleFromSamuraiWhenDisconnected();
            //AddNewSamuraiWithSecretIdentity();
            //AddSecretIdentityUsingSamuraiId();
            //AddSecretIdentityToExistingSamurai();
            //EdityASecretIdentity();
            //ReplaceASecretIdentity();
            //ReplaceASecretIdentityNotTracked();
            //ReplaceSecretIdentityNotInMemory();
            //CreateSamurai();
            //RetrieveSamuraisCreatedInPastWeek();
            //CreateThenEditSamuraiWithQuote();
            //GetAllSamurais();
            //CreateSamuraiWithBetterName();
            //RetrieveAndUpdateBetterName();
            //FixUpNullBetterName();
            //RetrieveScalarResult();
            //FilterWithScalarResult();
            //RetrieveDaysInBattle();
            GetStats();
            Filter();
            Project();
        }

        #region Working With Database Views
        private static void Project()
        {
            var stats = businessDbContext.SamuraiBattleStats.AsNoTracking()
                .Select(s => new
                {
                    s.Name,
                    s.NumberOfBattles
                })
                .ToList();
        }

        private static void Filter()
        {
            var stats = businessDbContext.SamuraiBattleStats.Where(s => s.SamuraiId == 2).AsNoTracking().ToList();
        }

        private static void GetStats()
        {
            var stats = businessDbContext.SamuraiBattleStats.AsNoTracking().ToList();
        }
        #endregion

        private static void RetrieveDaysInBattle()
        {
            var battles = businessDbContext.Battles.Select
                (b => new
                {
                    b.Name,
                    Days = BusinessDBContext.DaysInBattle(b.StartDate, b.EndDate)
                })
                .ToList();
        }

        private static void FilterWithScalarResult()
        {
            var samurais = businessDbContext.Samurais
                .Where(s => EF.Functions.Like(BusinessDBContext.EarliestBattleFoughtBySamurai(s.Id), "%Battle%"))
                .Select(s => s.Name)
                .ToList();
        }

        private static void RetrieveScalarResult()
        {
            var samurais = businessDbContext.Samurais
                .Select(s => new
                {
                    s.Name,
                    EarliestBattle = BusinessDBContext.EarliestBattleFoughtBySamurai(s.Id)
                })
                .ToList();

        }

        private static void FixUpNullBetterName()
        {
            var samurai = businessDbContext.Samurais.FirstOrDefault(s => s.Name == "Chrisjen");
            if (samurai is null) { return; }
            if(samurai.BetterName.IsEmpty())
            {
                samurai.BetterName = null;
            }
        }

        private static void RetrieveAndUpdateBetterName()
        {
            var samurai = businessDbContext.Samurais.FirstOrDefault(s => s.BetterName.lastName == "Black");
            //samurai.BetterName.firstName = "Jill";
            businessDbContext.SaveChanges();
        }

        private static void CreateSamuraiWithBetterName()
        {
            var samurai = new Samurai
            {
                Name = "Jack le Black",
                BetterName = PersonFullName.Create("Jack", "Black")
            };

            businessDbContext.Samurais.Add(samurai);
            businessDbContext.SaveChanges();
        }

        private static void GetAllSamurais()
        {
            var allSamurais = businessDbContext.Samurais.ToList(); 
        }

        private static void CreateThenEditSamuraiWithQuote()
        {
            var samurai = new Samurai { Name = "Ronin" };
            var quote = new Quote { Text = "Aren't I MARVELous?" };
            samurai.Quotes.Add(quote);
            businessDbContext.Samurais.Add(samurai);
            businessDbContext.SaveChanges();
            quote.Text += " See what I did there?";
            businessDbContext.SaveChanges();
        }

        private static void RetrieveSamuraisCreatedInPastWeek()
        {
            var oneWeekAgo = DateTime.Now.AddDays(-7);
            //var newSamurais = businessDbContext.Samurais
            //    .Where(s => EF.Property<DateTime>(s, "Created") >= oneWeekAgo)
            //    .ToList();
            var newSamurais = businessDbContext.Samurais
                .Where(s => EF.Property<DateTime>(s, "Created") >= oneWeekAgo)
                .Select(s => new { s.Id, s.Name, Created = EF.Property<DateTime>(s, "Created") })
                .ToList();
        }

        private static void CreateSamurai()
        {
            var samurai = new Samurai { Name = "Ronin" };
            businessDbContext.Samurais.Add(samurai);
            var timestamp = DateTime.Now;
            businessDbContext.Entry(samurai).Property("Created").CurrentValue = timestamp;
            businessDbContext.Entry(samurai).Property("LastModified").CurrentValue = timestamp;
            businessDbContext.SaveChanges();
        }

        private static void ReplaceSecretIdentityNotInMemory()
        {
            var samurai = businessDbContext.Samurais.FirstOrDefault(s => s.SecretIdentity != null);
            samurai.SecretIdentity = new SecretIdentity { RealName = "Bobbie Draper" };
            businessDbContext.SaveChanges();
        }

        private static void ReplaceASecretIdentityNotTracked()
        {
            Samurai samurai;
            using (var seperateOperation = new BusinessDBContext())
            {
                samurai = seperateOperation.Samurais.Include(s => s.SecretIdentity)
                    .FirstOrDefault(s => s.Id == 1);
            }
            samurai.SecretIdentity = new SecretIdentity { RealName = "Sampson" };
            businessDbContext.Samurais.Attach(samurai);
            businessDbContext.SaveChanges();
        }

        private static void ReplaceASecretIdentity()
        {
            var samurai = businessDbContext.Samurais.Include(s => s.SecretIdentity)
                .FirstOrDefault(s => s.Id == 1);
            samurai.SecretIdentity = new SecretIdentity { RealName = "Sampson" };
            businessDbContext.SaveChanges();
        }

        private static void EdityASecretIdentity()
        {
            var samurai = businessDbContext.Samurais.Include(s => s.SecretIdentity)
                .FirstOrDefault(s => s.Id == 1);
            samurai.SecretIdentity.RealName = "T'Challa";
            businessDbContext.SaveChanges();
        }

        private static void AddSecretIdentityToExistingSamurai()
        {
            Samurai samurai;
            using(var seprerateOperation = new BusinessDBContext())
            {
                samurai = businessDbContext.Samurais.Find(2);
            }
            samurai.SecretIdentity = new SecretIdentity { RealName = "Julia" };
            businessDbContext.Samurais.Attach(samurai);
            businessDbContext.SaveChanges();
        }

        private static void AddSecretIdentityUsingSamuraiId()
        {
            var identity = new SecretIdentity { SamuraiId = 1, };
            businessDbContext.Add(identity);
            businessDbContext.SaveChanges();
        }

        private static void AddNewSamuraiWithSecretIdentity()
        {
            var samurai = new Samurai { Name = "Jina Ujichika" };
            samurai.SecretIdentity = new SecretIdentity { RealName = "Julie" };
            businessDbContext.Add(samurai);
            businessDbContext.SaveChanges();
        }

        private static void RemoveBattleFromSamuraiWhenDisconnected()
        {
            Samurai samurai;
            using(var seperateOperation = new BusinessDBContext())
            {
                samurai = seperateOperation.Samurais.Include(s => s.SamuraiBattles)
                    .ThenInclude(sb => sb.Battle)
                    .SingleOrDefault(s => s.Id == 3);
            }

            var sbToRemove = samurai.SamuraiBattles.SingleOrDefault(sb => sb.BattleId == 1);
            samurai.SamuraiBattles.Remove(sbToRemove);
            //businessDbContext.Attach(samurai);
            //businessDbContext.ChangeTracker.DetectChanges();
            businessDbContext.Remove(sbToRemove);
            businessDbContext.SaveChanges();
        }

        private static void RemoveBattleFromSamurai()
        {
            //Goal: Remove join between shichiroji(id=3) and battle of okehazama (id=1)
            var samurai = businessDbContext.Samurais.Include(s => s.SamuraiBattles)
                .ThenInclude(sb => sb.Battle)
                .SingleOrDefault(s => s.Id == 3);
            var sbToRemove = samurai.SamuraiBattles.SingleOrDefault(sb => sb.BattleId == 1);
            samurai.SamuraiBattles.Remove(sbToRemove); //remove via list<t>
            //context.remove(sbtoremove); //remove using dbcontext
            businessDbContext.ChangeTracker.DetectChanges(); //here for debugging
            businessDbContext.SaveChanges();
        }

        private static void RemoveJoinBetweenSamuraiAndBattleSimple()
        {
            var join = new SamuraiBattle { BattleId = 1, SamuraiId = 8 };
            businessDbContext.Remove(join);
            businessDbContext.SaveChanges();
        }

        private static void InsertingDataInManyToManyRelationships()
        {
            PopulateDefaultDatabase();
            PopulateSamuraisAndBattles();
            JoinBattleAndSamurai();

            using (var context = new BusinessDBContext())
            {
                //EnlistSamuraiIntoABattle(context);
                //EnlistSamuraiIntoABattleUntracked(context);
                AddNewSamuraiViaDisconnectedBattleObject(context);
            }
        }

        private static void GetsamuraiWithBattles()
        {
            var samuraiWithBattles = businessDbContext.Samurais
                .Include(s => s.SamuraiBattles)
                .ThenInclude(sb => sb.Battle).FirstOrDefault(s => s.Id == 1);
            var Battle = samuraiWithBattles.SamuraiBattles.First().Battle;
            var allTheBattles = new List<Battle>();
            foreach (var samuraiBattle in samuraiWithBattles.SamuraiBattles)
            {
                allTheBattles.Add(samuraiBattle.Battle);
            }

        }

        private static void AddNewSamuraiViaDisconnectedBattleObject(BusinessDBContext context)
        {
            Battle battle;
            using(var seperateOperation = new BusinessDBContext())
            {
                battle = seperateOperation.Battles.Find(1);
            }

            var newSamurai = new Samurai { Name = "SampsonSan" };
            battle.SamuraiBattles.Add( new SamuraiBattle { Samurai = newSamurai});
            context.Battles.Attach(battle);
            context.SaveChanges();
        }

        private static void EnlistSamuraiIntoABattleUntracked(BusinessDBContext context)
        {
            Battle battle;
            using (var seperateOperation = new BusinessDBContext())
            {
                battle = seperateOperation.Battles.Find(1);
            }
            battle.SamuraiBattles.Add(new SamuraiBattle { SamuraiId = 2 });
            context.Battles.Attach(battle);
            context.ChangeTracker.DetectChanges();
            context.SaveChanges();
        }

        private static void EnlistSamuraiIntoABattle(BusinessDBContext context)
        {
            var battle = context.Battles.Find(1);
            battle.SamuraiBattles.Add(new SamuraiBattle { SamuraiId = 3 });
            context.SaveChanges();
        }

        private static void JoinBattleAndSamurai()
        {
            using (var context = new BusinessDBContext())
            {
                var sbJoin = new SamuraiBattle { SamuraiId = 1, BattleId = 3 };
                context.Add(sbJoin);
                context.SaveChanges();
            }
        }

        private static void PopulateSamuraisAndBattles()
        {
            using (var context = new BusinessDBContext())
            {
                context.Samurais.AddRange
                    (
                        new Samurai { Name = "Kikuchiyo" },
                        new Samurai { Name = "Kambei Shimada" },
                        new Samurai { Name = "Shichiroji" },
                        new Samurai { Name = "Katsushiro Okamoto" },
                        new Samurai { Name = "Heihachi Hayashida" },
                        new Samurai { Name = "Kyuzo" },
                        new Samurai { Name = "Gorobei Katayama" }
                    );

                context.Battles.AddRange
                    (
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

                    );

                context.SaveChanges();
            }

        }

        private static void PopulateDefaultDatabase()
        {
            InsertSamurai();
            InsertMultipleSamurais();
            InsertMultipleDifferentObjects();
            SimpleSamuraiQuery();
            MoreQueries();
            RetrieveAndUpdate();
            RetrieveAndUpdateMultipleSamurais();
            MultipleDatabaseOperations();
            InsertBattle();
            QueryAndUpdateBattle_Disconnected();
            DeleteWhileTracked();
            DeleteWhileNotTracked();
            InsertNewPkFkGraph();
            InsertNewPkFkGraphMultipleChildren();
            AddChildToExistingObjectWhileTracked();
            AddChildToExistingObjectWhileNotTracked(1);
            EagerLoadSamuraiWithQuotes();
            ProjectSomeProperties();
            ModifyingRelatedDataWhenTracked();
            ModifyingRelatedDataWhenNotTracked();
        }

        private static void ModifyingRelatedDataWhenNotTracked()
        {
            using (var context = new BusinessDBContext())
            {
                var samurai = context.Samurais.Include(s => s.Quotes).FirstOrDefault();
                var quote = samurai.Quotes[0];
                quote.Text += " Did you hear that?";
                using(var newContext = new BusinessDBContext())
                {
                    newContext.Entry(quote).State = EntityState.Modified;
                    //newContext.Quotes.Update(quote);
                    newContext.SaveChanges();
                }
            }
        }

        private static void ModifyingRelatedDataWhenTracked()
        {
            using (var context = new BusinessDBContext())
            {
                var samurai = context.Samurais.Include(s => s.Quotes).FirstOrDefault();
                samurai.Quotes[0].Text += " Did you hear that?";
                context.SaveChanges();
            }
        }

        private static void ProjectSomeProperties()
        {
            using (var context = new BusinessDBContext())
            {
                var someProperties = context.Samurais.Select(s => new { s.Id, s.Name, s.Quotes }).ToList();
                foreach (var item in someProperties)
                {
                    Console.WriteLine(item);
                };
            }
            
        }

        private static void EagerLoadSamuraiWithQuotes()
        {
            using (var context = new BusinessDBContext())
            {
                var samuraiWithQuotes = context.Samurais.Include(s => s.Quotes).ToList();
            }
            
        }

        private static void AddChildToExistingObjectWhileNotTracked(int samuraiId = 0)
        {
            using (var context = new BusinessDBContext())
            {
                var quote = new Quote
                {
                    Text = "Now that I saved you, will you feed me dinner?",
                    SamuraiId = samuraiId
                };
                using (var newContext = new BusinessDBContext())
                {
                    newContext.Quotes.Add(quote);
                    newContext.SaveChanges();
                }
            }
           
        }

        private static void AddChildToExistingObjectWhileTracked()
        {
            using (var context = new BusinessDBContext())
            {
                var samurai = context.Samurais.FirstOrDefault();
                samurai.Quotes.Add(new Quote
                {
                    Text = "I bet you're happy that I've saved you!"
                });

                context.SaveChanges();
            }
        }

        private static void InsertNewPkFkGraphMultipleChildren()
        {
            using (var context = new BusinessDBContext())
            {
                var samurai = new Samurai
                {
                    Name = "Kyuzo",
                    Quotes = new List<Quote>
                    {
                        new Quote
                        {
                            Text = "Watch out for my sharp sword!"
                        },

                        new Quote
                        {
                            Text = "I told you to watch out for the sharp sword! Oh well!"
                        }
                    }
                };
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }
        }

        private static void InsertNewPkFkGraph()
        {
            using (var context = new BusinessDBContext())
            {
                var samurai = new Samurai
                {
                    Name = "Kambei Shimada",
                    Quotes = new List<Quote>
                    {
                        new Quote
                        {
                            Text = "I've come to save you"
                        }
                    }
                };
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }
        }

        private static void DeleteWhileNotTracked()
        {
            using (var context = new BusinessDBContext())
            {
                var samurai = context.Samurais.FirstOrDefault(s => s.Name == "Heihachi Hayashida");
                using (var newContextInstance = new BusinessDBContext())
                {
                    if (samurai != null)
                    {
                        newContextInstance.Samurais.Remove(samurai);
                    }

                    newContextInstance.SaveChanges();
                }
            }
        }

        private static void DeleteWhileTracked()
        {
            using (var context = new BusinessDBContext())
            {
                var samurai = context.Samurais.FirstOrDefault(s => s.Name == "Kambei Shimada");
                if(samurai != null)
                {
                    context.Samurais.Remove(samurai);
                }
                context.SaveChanges();
            }
        }

        private static void QueryAndUpdateBattle_Disconnected()
        {
            using (var context = new BusinessDBContext())
            {
                var battle = context.Battles.FirstOrDefault();
                battle.EndDate = new DateTime(1560, 06, 30);
                using (var newContextInstance = new BusinessDBContext())
                {
                    newContextInstance.Battles.Update(battle);
                    newContextInstance.SaveChanges();
                }
            }

        }

        private static void InsertBattle()
        {
            using (var context = new BusinessDBContext())
            {
                context.Battles.Add(new Battle
                {
                    Name = "Battle of Okehazama",
                    StartDate = new DateTime(1560, 05, 01),
                    EndDate = new DateTime(1560, 06, 15)
                });

                context.SaveChanges();
            }
        }

        private static void MultipleDatabaseOperations()
        {
            using (var context = new BusinessDBContext())
            {
                var samurai = context.Samurais.FirstOrDefault();
                samurai.Name += "Hiro";
                context.Samurais.Add(new Samurai { Name = "Kikuchiyo" });
                context.SaveChanges();
            }
        }

        private static void RetrieveAndUpdateMultipleSamurais()
        {
            using (var context = new BusinessDBContext())
            {
                var samurais = context.Samurais.ToList();
                samurais.ForEach(s => s.Name += "San");
                context.SaveChanges();
            }
        }

        private static void RetrieveAndUpdate()
        {
            using (var context = new BusinessDBContext())
            {
                var samurais = context.Samurais.FirstOrDefault();
                samurais.Name += "San";
                context.SaveChanges();
            }
            
        }

        private static void MoreQueries()
        {
            using (var context = new BusinessDBContext())
            {
                var name = "Sampson";
                var samurais = context.Samurais.FirstOrDefault(s => s.Name == name);
            }
        }

        private static void SimpleSamuraiQuery()
        {
            using(var context = new BusinessDBContext())
            {
                var samurais = context.Samurais.ToList();
                foreach (var samurai in samurais)
                {
                    Console.WriteLine(samurai.Name);
                }
            }
        }

        private static void InsertMultipleDifferentObjects()
        {
            var samurai = new Samurai { Name = "Oda Nobunaga" };
            var battle = new Battle
            {
                Name = "Battle of Nagashino",
                StartDate = new DateTime(1575, 06, 16),
                EndDate = new DateTime(1575, 06, 28)
            };

            using (var context = new BusinessDBContext())
            {
                context.AddRange(samurai, battle);
                context.SaveChanges();
            }
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Julie" };
            var samuraiSammy = new Samurai { Name = "Sampson" };
            var samuraiList = new List<Samurai>();

            samuraiList.Add(samurai);
            samuraiList.Add(samuraiSammy);

            using (var context = new BusinessDBContext())
            {
                context.Samurais.AddRange(samuraiList);
                context.SaveChanges();
            }
        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai { Name = "Julie" };
            using (var context = new BusinessDBContext())
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }
        }
        #endregion
    }

}
