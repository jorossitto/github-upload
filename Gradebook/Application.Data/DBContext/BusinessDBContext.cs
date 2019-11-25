using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;
using System.Diagnostics.Contracts;
using Microsoft.Extensions.Configuration;
using AppCore.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Drawing;
using AppCore.Entities;

namespace AppCore.Data
{
    //Grab SQLite/Sql server compact toolbox
    public class BusinessDBContext : IdentityDbContext<IdentityUser>
    {
        #region mocks
        MockCategoryRepository mockCategoryRepository = new MockCategoryRepository();
        MockPieRepository mockPieRepository = new MockPieRepository();
        #endregion mocks

        #region Variables
        private readonly IConfiguration _config;
        private readonly LoggingType loggingType = LoggingType.Sql;
        #endregion


        //Depricated
        //var loggerFactory = new LoggerFactory(new[] { new ConsoleLoggerProvider((category, level) => level >= LogLevel.Information, true) });

        #region constructors
        //Default Constructor
        public BusinessDBContext()
        {
            AddEvents();
        }

        public BusinessDBContext(DbContextOptions<BusinessDBContext> options,
            IConfiguration config = null) : base(options)
        {
            _config = config;
            AddEvents();
        }
        #endregion constructors

        #region DBSets
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Pie> Pies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Camp> Camps { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Talk> Talks { get; set; }
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle>Battles { get; set; }
        public DbSet<SamuraiStat> SamuraiBattleStats { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<BrewerType> BrewerTypes { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Country> Countries { get; set; }
        //public DbSet<Recipe> Recipes { get; set; }
        //public DbSet<UnitQueryType> UnitsInService { get; set; }

        #endregion



        #region Logging
        private ILoggerFactory GetSQLLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information)
                          //.AddFilter(DbLoggerCategory.Database.Command.Name,
                          //           LogLevel.Debug)
                          );
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }

        private ILoggerFactory GetChangeTrackerLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder.AddConsole()
            .AddFilter(DbLoggerCategory.ChangeTracking.Name, LogLevel.Debug));

            return serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Contract.Requires(optionsBuilder != null);

            if (optionsBuilder.IsConfigured == false)
            {
                if(loggingType == LoggingType.Sql)
                {
                    optionsBuilder.UseLoggerFactory(GetSQLLoggerFactory())
                        .EnableSensitiveDataLogging(true)
                        .UseSqlServer(config.ExplicitDatabaseConnection);
                        //.UseLazyLoadingProxies();
                }
                else
                {
                    optionsBuilder.UseLoggerFactory(GetChangeTrackerLoggerFactory())
                        .EnableSensitiveDataLogging(true)
                        .UseSqlServer(config.ExplicitDatabaseConnection);
                        //.UseLazyLoadingProxies();
                }
            }
            else
            {
                //if(_config == null)
                //{
                //    //optionsBuilder.UseLoggerFactory(GetLoggerFactory())
                //    //    .EnableSensitiveDataLogging(true)
                //    //    .UseSqlServer(config.ExplicitDatabaseConnection);
                //}
                //else
                //{
                //    optionsBuilder.UseLoggerFactory(GetLoggerFactory())
                //        .EnableSensitiveDataLogging(true)
                //        .UseSqlServer(_config.GetConnectionString(config.DefaultConnection));
                //}

            }
        }
        
        #region dbFunctions
        [DbFunction(Schema = "dbo")]
        public static string EarliestBattleFoughtBySamurai(int samuraiId)
        {
            throw new Exception();
        }

        [DbFunction(Schema = "dbo")]
        public static int DaysInBattle(DateTime start, DateTime end)
        {
            return (int)end.Subtract(start).TotalDays + 1;
        }
        #endregion

        #region events
        private void AddEvents()
        {
            ChangeTracker.StateChanged += Statechanged;
            ChangeTracker.Tracked += Tracked;
        }

        private void Statechanged(object sender, EntityStateChangedEventArgs e)
        {
            int keyValue = GetKeyValue(e.Entry.Entity);
            Console.WriteLine($@"State of {e.Entry.Entity.GetType()} with Key = {keyValue} changed from " +
                $"{e.OldState} to {e.NewState}");
        }

        private void Tracked(object sender, EntityTrackedEventArgs e)
        {
            int keyValue = GetKeyValue(e.Entry.Entity);
            Console.WriteLine($@"Newly tracked {e.Entry.Entity.GetType()}, " +
                $"{((e.FromQuery) ? "From" : "Not From")} Query, Key = {keyValue} as {e.Entry.State}");
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Contract.Requires(modelBuilder != null);

            base.OnModelCreating(modelBuilder);

            CreateCatagoryData(modelBuilder);

            CreatePieData(modelBuilder);

            CreateCampData(modelBuilder);

            SeedLocationData(modelBuilder);

            CreateBattleData(modelBuilder);

            SamuraiMethods(modelBuilder);

            AddShadowProperties(modelBuilder);


            SeedRecipe(modelBuilder);
            SeedUnit(modelBuilder);
            SeedBrewerType(modelBuilder);
            SeedEmployee(modelBuilder);
            SeedConference(modelBuilder);
            SeedSession(modelBuilder);
            SeedSessionTag(modelBuilder);
            SeedReach(modelBuilder);
            SeedSpeaker(modelBuilder);
            SeedTalk(modelBuilder);
        }







        #region Building Model
        private void SeedTalk(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Talk>()
              .HasData(new
              {
                  TalkId = 1,
                  CampId = 1,
                  SpeakerId = 1,
                  Title = "Entity Framework From Scratch",
                  Abstract = "Entity Framework from scratch in an hour. Probably cover it all",
                  Level = 100,
                  Created = new DateTime(2018, 6, 1),
                  LastModified = new DateTime(2018, 6, 1)
              },
              new
              {
                  TalkId = 2,
                  CampId = 1,
                  SpeakerId = 2,
                  Title = "Writing Sample Data Made Easy",
                  Abstract = "Thinking of good sample data examples is tiring.",
                  Level = 200,
                  Created = new DateTime(2018, 6, 1),
                  LastModified = new DateTime(2018, 6, 1)
              });
        }
        private void SeedSpeaker(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Speaker>()
              .HasData(new
              {
                  SpeakerId = 1,
                  FirstName = "Shawn",
                  LastName = "Wildermuth",
                  BlogUrl = "http://wildermuth.com",
                  Company = "Wilder Minds LLC",
                  CompanyUrl = "http://wilderminds.com",
                  GitHub = "shawnwildermuth",
                  Twitter = "shawnwildermuth",
                  ConferenceId = 1
              },
                  new
                  {
                      SpeakerId = 2,
                      FirstName = "Resa",
                      LastName = "Wildermuth",
                      BlogUrl = "http://shawnandresa.com",
                      Company = "Wilder Minds LLC",
                      CompanyUrl = "http://wilderminds.com",
                      GitHub = "resawildermuth",
                      Twitter = "resawildermuth",
                      ConferenceId = 1
                  });
        }
        private void SeedReach(ModelBuilder modelBuilder)
        {
            var reach = modelBuilder.Entity<Reach>();
            reach.Property(x => x.ReachId).ValueGeneratedNever();
            reach.HasData
                (
                    new Reach { ReachId = (int)ReachId.Keynote, Description = "Keynote" },
                    new Reach { ReachId = (int)ReachId.Breakout, Description = "Breakout" },
                    new Reach { ReachId = (int)ReachId.OpenSpace, Description = "Open Space" }
                );
        }

        private void SeedSessionTag(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionTag>().HasKey(x => new { x.SessionId, x.Tag });
        }

        private void SeedSession(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>().HasAlternateKey(x => x.SessionGuid);
        }

        private void SeedConference(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conference>().HasAlternateKey(x => x.Identifier);
        }

        private void SeedRecipe(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Recipe>().Property(r => r.TotalBrewTime)
            //    .HasConversion(new TimeSpanToTicksConverter());
            //modelBuilder.Entity<Recipe>().Property(r => r.TotalBrewTime).HasConversion<System.Int64>();
        }
        private void SeedEmployee(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData
                (
                    new Employee { EmployeeId = 1, Name = "Leia", LocationId = 1, Barista = true },
                    new Employee { EmployeeId = 2, Name = "Rey", LocationId = 2, Barista = true },
                    new Employee { EmployeeId = 3, Name = "Gamora", LocationId = 2, Barista = true },
                    new Employee { EmployeeId = 4, Name = "Dr. Strange", LocationId = 3, Barista = true },
                    new Employee { EmployeeId = 5, Name = "Peter Parker", LocationId = 3, Barista = false }
                );
        }

        private void SeedBrewerType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrewerType>().Property(b => b.Color)
                .HasConversion(new ColorToStringValueConverter());

            #region Data
            modelBuilder.Entity<BrewerType>().HasData
                (
                    new BrewerType { BrewerTypeId = 1, Description = "Glass Hourglass Drip" },
                    new BrewerType { BrewerTypeId = 2, Description = "Hand Press" },
                    new BrewerType { BrewerTypeId = 3, Description = "Cold Brew" }
                );
            modelBuilder.Entity<BrewerType>().OwnsOne(b => b.Recipe);
            modelBuilder.Entity<BrewerType>().OwnsOne(b => b.Recipe).HasData(
                new
                {
                    BrewerTypeId =1,
                    BrewMinutes = 3,
                    GrindSize = 2,
                    GrindOunces = 2,
                    WaterOunces = 9,
                    WaterTemperatureF = 130,
                    Description = "So good!",
                    TotalBrewTime = TimeSpan.FromMinutes(3)
                });

            modelBuilder.Entity<BrewerType>().OwnsOne(b => b.Recipe).HasData(
                new
                {
                    BrewerTypeId = 2,
                    BrewMinutes = 1,
                    GrindSize = 2,
                    GrindOunces = 2,
                    WaterOunces = 9,
                    WaterTemperatureF = 130,
                    Description = "Love a hand pressed coffee!",
                    TotalBrewTime = TimeSpan.FromMinutes(1)
                });

            modelBuilder.Entity<BrewerType>().OwnsOne(b => b.Recipe).HasData(
                new
                {
                    BrewerTypeId = 3,
                    BrewMinutes = 60,
                    GrindSize = 2,
                    GrindOunces = 2,
                    WaterOunces = 9,
                    WaterTemperatureF = 130,
                    Description = "Cold brew is worth the wait!",
                    TotalBrewTime = TimeSpan.FromMinutes(60)
                });
            #endregion
        }

        private void SeedUnit(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>().Property(u => u.Acquired).HasColumnType("Date");
            modelBuilder.Entity<Unit>().HasData
                (
                    new  { UnitId = 1, Acquired = new DateTime(2018, 6, 1), LocationId = 1, BrewerTypeId = 2,
                        Created = new DateTime(2018, 6, 1), LastModified = new DateTime(2018, 6, 1), Cost = 99m
                    },
                    new  { UnitId = 2, Acquired = new DateTime(2018, 6, 2), LocationId = 1, BrewerTypeId = 3,
                        Created = new DateTime(2018, 6, 1), LastModified = new DateTime(2018, 6, 1),
                        Cost = 99m
                    },
                    new  { UnitId = 3, Acquired = new DateTime(2018, 6, 3), LocationId = 1, BrewerTypeId = 1,
                        Created = new DateTime(2018, 6, 1), LastModified = new DateTime(2018, 6, 1),
                        Cost = 99m
                    },
                    new  { UnitId = 4, Acquired = new DateTime(2018, 6, 4), LocationId = 2, BrewerTypeId = 1,
                        Created = new DateTime(2018, 6, 1), LastModified = new DateTime(2018, 6, 1),
                        Cost = 99m
                    }
                );
        }
        private void SeedLocationData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().Property(l => l.LocationType)
                .HasConversion(new EnumToStringConverter<LocationType>());

            modelBuilder.Entity<Location>()
              .HasData(new
              {
                  LocationId = 1,
                  VenueName = "Atlanta Convention Center",
                  Address1 = "123 Main Street",
                  CityTown = "Atlanta",
                  StateProvince = "GA",
                  PostalCode = "12345",
                  Country = "USA",
                  OpenTime = "5am",
                  CloseTime = "5pm",
                  LocationType = LocationType.Kiosk

              });

            modelBuilder.Entity<Location>()
              .HasData(new
              {
                  LocationId = 2,
                  VenueName = "Atlanta Convention Center",
                  Address1 = "999 Main Street",
                  CityTown = "Atlanta",
                  StateProvince = "GA",
                  PostalCode = "12345",
                  Country = "USA",
                  OpenTime = "6am",
                  CloseTime = "6pm",
                  LocationType = LocationType.Storefront

              });

            modelBuilder.Entity<Location>().HasData(new
            {
                LocationId = 3,
                Address1 = "3 Main",
                OpenTime = "7am",
                CloseTime = "7pm",
                LocationType = LocationType.Popup
            });

        }
        private void AddShadowProperties(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {

                Console.WriteLine($"Entity: {entityType.Name}");
                if (entityType.Name != "AppCore.Entities.Camp"
                    && entityType.Name != "AppCore.Data.Category"
                    && entityType.Name != "AppCore.Entities.Location"
                    && entityType.Name != "AppCore.Data.Pie"
                    && entityType.Name != "AppCore.Entities.Speaker"
                    && entityType.Name != "AppCore.Data.BrewerType"
                    && entityType.Name != "AppCore.Data.Recipe"
                    && entityType.Name != "AppCore.Entities.Employee"
                    && entityType.ClrType.BaseType != typeof(DbView))
                {
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("Created");
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModified");
                }



            }
        }

        #region SamuraiMethods
        private void SamuraiMethods(ModelBuilder modelBuilder)
        {
            CreateSamuraiBattleData(modelBuilder);

            CreateSamuraiData(modelBuilder);

            CreateSamuraiStatData(modelBuilder);
        }
        private void CreateSamuraiStatData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiStat>().HasKey(s => s.SamuraiId);
        }
        private static void CreateSamuraiData(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Samurai>().HasOne(s => s.SecretIdentity)
            //    .WithOne(i => i.samurai).HasForeignKey<SecretIdentity>("SamuraiID");

            //modelBuilder.Entity<Samurai>().Property<DateTime>("Created");
            //modelBuilder.Entity<Samurai>().Property<DateTime>("LastModified");
            //modelBuilder.Entity<Samurai>().OwnsOne(s => s.BetterName).ToTable("BetterNames");
            //modelBuilder.Entity<Samurai>().OwnsOne(s => s.BetterName).Property(b => b.firstName)
            //    .HasColumnName("FirstName");
            //modelBuilder.Entity<Samurai>().OwnsOne(s => s.BetterName).Property(b => b.lastName)
            //    .HasColumnName("LastName");
        }

        private static void CreateSamuraiBattleData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>().HasKey(s => new { s.SamuraiId, s.BattleId });
        }
        #endregion

        private static void CreateBattleData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Battle>().Property(b => b.StartDate).HasColumnType(config.Date);
            modelBuilder.Entity<Battle>().Property(b => b.EndDate).HasColumnType(config.Date);
        }

        private static void CreateCatagoryData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1, CategoryName = "Fruit pies", Description = "All-Fruit Pies" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, CategoryName = "Cheese cakes", Description = "Cheesy all the way" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, CategoryName = "Seasonal Pies", Description = "Get in the mood for a seasonal pie" });
        }

        private void CreatePieData(ModelBuilder modelBuilder)
        {
            var myPies = mockPieRepository.AllPies;

            modelBuilder.Entity<Pie>().HasData(new Pie
            {
                PieId = 1,
                Name = "Apple Pie",
                Price = 12.95M,
                ShortDescription = "Our famous apple pies!",
                LongDescription =
                    "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                CategoryId = 1,
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepie.jpg",
                InStock = true,
                IsPieOfTheWeek = true,
                ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepiesmall.jpg",
                AllergyInformation = ""
            });

            modelBuilder.Entity<Pie>().HasData(new Pie
            {
                PieId = 2,
                Name = "Blueberry Cheese Cake",
                Price = 18.95M,
                ShortDescription = "You'll love it!",
                LongDescription =
                    "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                CategoryId = 2,
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecake.jpg",
                InStock = true,
                IsPieOfTheWeek = false,
                ImageThumbnailUrl =
                    "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecakesmall.jpg",
                AllergyInformation = ""
            });

            modelBuilder.Entity<Pie>().HasData(new Pie
            {
                PieId = 3,
                Name = "Cheese Cake",
                Price = 18.95M,
                ShortDescription = "Plain cheese cake. Plain pleasure.",
                LongDescription =
                    "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                CategoryId = 2,
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecake.jpg",
                InStock = true,
                IsPieOfTheWeek = false,
                ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg",
                AllergyInformation = ""
            });

            modelBuilder.Entity<Pie>().HasData(new Pie
            {
                PieId = 4,
                Name = "Cherry Pie",
                Price = 15.95M,
                ShortDescription = "A summer classic!",
                LongDescription =
                    "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                CategoryId = 1,
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypie.jpg",
                InStock = true,
                IsPieOfTheWeek = false,
                ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypiesmall.jpg",
                AllergyInformation = ""
            });

            modelBuilder.Entity<Pie>().HasData(new Pie
            {
                PieId = 5,
                Name = "Christmas Apple Pie",
                Price = 13.95M,
                ShortDescription = "Happy holidays with this pie!",
                LongDescription =
                    "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                CategoryId = 3,
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepie.jpg",
                InStock = true,
                IsPieOfTheWeek = false,
                ImageThumbnailUrl =
                    "https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepiesmall.jpg",
                AllergyInformation = ""
            });

            modelBuilder.Entity<Pie>().HasData(new Pie
            {
                PieId = 6,
                Name = "Cranberry Pie",
                Price = 17.95M,
                ShortDescription = "A Christmas favorite",
                LongDescription =
                    "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                CategoryId = 3,
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypie.jpg",
                InStock = true,
                IsPieOfTheWeek = false,
                ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypiesmall.jpg",
                AllergyInformation = ""
            });

            modelBuilder.Entity<Pie>().HasData(new Pie
            {
                PieId = 7,
                Name = "Peach Pie",
                Price = 15.95M,
                ShortDescription = "Sweet as peach",
                LongDescription =
                    "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                CategoryId = 1,
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpie.jpg",
                InStock = false,
                IsPieOfTheWeek = false,
                ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpiesmall.jpg",
                AllergyInformation = ""
            });

            modelBuilder.Entity<Pie>().HasData(new Pie
            {
                PieId = 8,
                Name = "Pumpkin Pie",
                Price = 12.95M,
                ShortDescription = "Our Halloween favorite",
                LongDescription =
                    "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                CategoryId = 3,
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpie.jpg",
                InStock = true,
                IsPieOfTheWeek = true,
                ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpiesmall.jpg",
                AllergyInformation = ""
            });


            modelBuilder.Entity<Pie>().HasData(new Pie
            {
                PieId = 9,
                Name = "Rhubarb Pie",
                Price = 15.95M,
                ShortDescription = "My God, so sweet!",
                LongDescription =
                    "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                CategoryId = 1,
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg",
                InStock = true,
                IsPieOfTheWeek = true,
                ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpiesmall.jpg",
                AllergyInformation = ""
            });

            modelBuilder.Entity<Pie>().HasData(new Pie
            {
                PieId = 10,
                Name = "Strawberry Pie",
                Price = 15.95M,
                ShortDescription = "Our delicious strawberry pie!",
                LongDescription =
                    "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                CategoryId = 1,
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg",
                InStock = true,
                IsPieOfTheWeek = false,
                ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg",
                AllergyInformation = ""
            });

            modelBuilder.Entity<Pie>().HasData(new Pie
            {
                PieId = 11,
                Name = "Strawberry Cheese Cake",
                Price = 18.95M,
                ShortDescription = "You'll love it!",
                LongDescription =
                    "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                CategoryId = 2,
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecake.jpg",
                InStock = false,
                IsPieOfTheWeek = false,
                ImageThumbnailUrl =
                    "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecakesmall.jpg",
                AllergyInformation = ""
            });

            //foreach (var pie in myPies)
            //{
            //    modelBuilder.Entity<Pie>().HasData(pie);
            //}
        }

        private static void CreateCampData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camp>()
                  .HasData(new
                  {
                      CampId = 1,
                      Moniker = "ATL2018",
                      Name = "Atlanta Code Camp",
                      EventDate = new DateTime(2018, 10, 18),
                      LocationId = 1,
                      Length = 1
                  });
        }
        #endregion

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var timestamp = DateTime.Now;
            foreach(var entity in ChangeTracker.Entries()
                .Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified)
                && !e.Metadata.IsOwned()))
                
            {
                if(entity.GetType().GetProperty(config.LastModified) != null)
                {
                    entity.Property(config.LastModified).CurrentValue = timestamp;
                }

                if(entity.State == EntityState.Added)
                {
                    if(entity.GetType().GetProperty(config.Created) != null)
                    {
                        entity.Property(config.Created).CurrentValue = timestamp;
                    }
                }

                //if Owned
                //if (entity.Entity is Samurai)
                //{
                //    if (entity.Reference("BetterName").CurrentValue == null)
                //    {
                //        entity.Reference("BetterName").CurrentValue = PersonFullName.Empty();
                //    }
                //}
            }
            return base.SaveChanges();
        }

        private int GetKeyValue<T>(T entity)
        {
            try
            {
                var etype = Model.FindEntityType(entity.GetType());
                var pkey = etype.FindPrimaryKey();
                var props = pkey.Properties;
                var keyName = Model.FindEntityType(entity.GetType()).FindPrimaryKey().Properties
                    .Select(x => x.Name).Single();

                return (int)entity.GetType().GetProperty(keyName).GetValue(entity, null);
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        #region ValueConverters
        private ValueConverter<Color, string> ColorConverter = 
            new ValueConverter<Color, string>(c => c.Name, s => Color.FromName(s));

        #endregion

    }
}
