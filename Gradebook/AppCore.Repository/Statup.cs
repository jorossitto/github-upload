using AppCore.Entities;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using AppCore.Data;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Repository
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<BusinessDBContext>(opt =>
            opt.UseSqlite(connectionString));

        }
    }
}
