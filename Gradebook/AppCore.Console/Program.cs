using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.Data;
using AppCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace AppCore.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //PopulateDefaultDatabase();
            PopulateSamuraisAndBattles();
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
    }
}
