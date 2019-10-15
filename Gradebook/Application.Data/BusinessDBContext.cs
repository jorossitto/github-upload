using ACM.BL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class BusinessDBContext : DbContext
    {
        MockCategoryRepository mockCategoryRepository = new MockCategoryRepository();
        MockPieRepository mockPieRepository = new MockPieRepository();

        public BusinessDBContext(DbContextOptions<BusinessDBContext> options) : base(options)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Pie> Pies { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Contract.Requires(modelBuilder != null);

            base.OnModelCreating(modelBuilder);
            var myPies = mockPieRepository.AllPies;

            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1, CategoryName = "Fruit pies", Description = "All-Fruit Pies" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, CategoryName = "Cheese cakes", Description = "Cheesy all the way" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, CategoryName = "Seasonal Pies", Description = "Get in the mood for a seasonal pie" });

            foreach (var pie in myPies)
            {
                modelBuilder.Entity<Pie>().HasData(pie);
            }
            
        }
    }
}
