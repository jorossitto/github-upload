using AppCore.Data;
using AppCore.Domain;
using AppCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace AppCore.Api.XunitTests
{
    public class AuthorREpositoryTests
    {
        private readonly ITestOutputHelper output;
        public AuthorREpositoryTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void GetAuthors_PageSizeisThree_ReturnsThreeAuthors()
        {
            var options = CreateSQLliteInMemoryDatabase();
            //Open Connection Add Data 
            using (var context = new BusinessDBContext(options))
            {
                OpenConnectionAndEnsureCreated(context);

                AddBelgium(context);

                context.Countries.Add(new Country()
                {
                    Id = "US",
                    Description = "United States of America"
                });

                context.Authors.Add(new Author()
                {
                    FirstName = "Kevin",
                    LastName = "Dockx",
                    CountryId = "BE"
                });
                context.Authors.Add(new Author()
                {
                    FirstName = "Gill",
                    LastName = "Cleeren",
                    CountryId = "BE"
                });
                context.Authors.Add(new Author()
                {
                    FirstName = "Julie",
                    LastName = "Lerman",
                    CountryId = "US"
                });
                context.Authors.Add(new Author()
                {
                    FirstName = "Shawn",
                    LastName = "Wildermuth",
                    CountryId = "BE"
                });
                context.Authors.Add(new Author()
                {
                    FirstName = "Deborah",
                    LastName = "Kurata",
                    CountryId = "US"
                });

                context.SaveChanges();
            }
            //Query the database
            using(var context = new BusinessDBContext(options))
            {
                var authorRepository = new AuthorRepository(context);

                //Act
                var authors = authorRepository.GetAuthors(1, 3);

                //Assert
                Assert.Equal(3, authors.Count());
            }
        }

        [Fact]
        public void GetAuthor_EmptyGuid_ThrowsArgumentException()
        {
            var options = CreateSQLliteInMemoryDatabase();

            using (var context = new BusinessDBContext(options))
            {
                OpenConnectionAndEnsureCreated(context);
                var authorRepository = new AuthorRepository(context);
                Assert.Throws<ArgumentException>(
                    //Act
                    () => authorRepository.GetAuthor(Guid.Empty));
            }
        }

        [Fact]
        public void AddAuthor_AuthorWithoutCountryId_AuthorHasBeAsCountryId()
        {
            var options = CreateSQLliteInMemoryDatabase();
            var newGuid = Guid.NewGuid();

            //Open Connection and Add Belgim
            using (var context = new BusinessDBContext(options))
            {
                OpenConnectionAndEnsureCreated(context);
                AddBelgium(context);
                context.SaveChanges();
            }

            //Add Author
            using (var context = new BusinessDBContext(options))
            {
                var authorRepository = new AuthorRepository(context);
                var authorToAdd = new Author()
                {
                    FirstName = "Deborah",
                    LastName = "Kurata",
                    Id = newGuid
                };

                //Act
                authorRepository.AddAuthor(authorToAdd);
                authorRepository.SaveChanges();
            }

            //Querry Database and Verify Test Results
            using (var context = new BusinessDBContext(options))
            {
                //assert
                var authorRepository = new AuthorRepository(context);
                var addedAuthor = authorRepository.GetAuthor(newGuid);
                Assert.Equal("BE", addedAuthor.CountryId);
            }
        }

        #region TestHelpers
        #region DatabaseCreation
        private DbContextOptions<BusinessDBContext> CreateSQLliteInMemoryDatabase()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = ":memory:"
            };

            var connection = new SqliteConnection(connectionStringBuilder.ToString());

            return new DbContextOptionsBuilder<BusinessDBContext>()
                .UseLoggerFactory(new LoggerFactory(new[]
                    {
                        new LogToActionLoggerProvider((log) =>
                        {
                            output.WriteLine(log);
                        })
                    }))
                .UseSqlite(connection)
                .Options;
        }

        private static DbContextOptions<BusinessDBContext> CreateInMemoryDatabase()
        {
            return new DbContextOptionsBuilder<BusinessDBContext>()
                .UseInMemoryDatabase($"AuthorRepositoryForTesting{Guid.NewGuid()}")
                .Options;
        }

        private static void OpenConnectionAndEnsureCreated(BusinessDBContext context)
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
        }

        #endregion

        private static void AddBelgium(BusinessDBContext context)
        {
            context.Countries.Add(new Country()
            {
                Id = "BE",
                Description = "Belgium"
            });
        }


        #endregion
    }
}
