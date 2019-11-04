using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACM.BL;
using AppCore.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BusinessSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContextPool<BusinessDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(config.BusinessDatabaseConnection));
            });

            //services.AddDbContext<CampContext>();
            //services.AddScoped<ICampRepository, CampRepository>();

            //In Memory Connection for testing only
            //services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();
            //Bethany's Pie Shops
            services.AddScoped<IPieRepository, MockPieRepository>();
            services.AddScoped<ICategoryRepository, MockCategoryRepository>();

            //services.addTransient
            //services.AddSingleton

            //SQL Connection
            //Restaurants
            services.AddScoped<IRestaurantData, SqlRestaurantData>();




            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllersWithViews();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.Use(SayHelloMiddleWare);

            app.UseHttpsRedirection();
            app.UseNodeModules(TimeSpan.FromSeconds(600));
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute
                //(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}"
                //);
                endpoints.MapRazorPages();
            });
        }

        private RequestDelegate SayHelloMiddleWare(RequestDelegate next)
        {
            return async context =>
            {
                if(context.Request.Path.StartsWithSegments("/hello"))
                {
                    await context.Response.WriteAsync("Hello, World!");
                }
                else
                {
                    await next(context);
                }
            };
            
        }
    }
}
