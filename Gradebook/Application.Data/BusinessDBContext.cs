using ACM.BL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class BusinessDBContext : DbContext
    {
        public BusinessDBContext(DbContextOptions<BusinessDBContext> options) : base(options)
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }

    }
}
