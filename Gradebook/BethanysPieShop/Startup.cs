using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Data;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BethanysPieShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BusinessDBContext>
                (
                    options => options.UseSqlServer
                    (Configuration.GetConnectionString(config.DefaultConnection))
                );

            //Mock Repository
            //services.AddScoped<ICategoryRepository, MockCategoryRepository>();
            //services.AddScoped<IPieRepository, MockPieRepository>();

            //Real Repository
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<BusinessDBContext>();

            services.AddScoped<IPieRepository, PieRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));

            services.AddScoped<ICampRepository, CampRepository>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();

            services.AddAutoMapper(typeof(Startup));

            //services.AddApiVersioning();

            services.AddHttpContextAccessor();
            services.AddSession();
            //services.AddMvc(); would also work still
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();//must be before use routing

            app.UseRouting();
            app.UseAuthentication();//Must be after Routing
            app.UseAuthorization();//Must be after Authentication

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
