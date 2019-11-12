using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BusinessSample.UI
{
    public class Program
    {
        //public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile(path: config.AppSettingsJson, optional: false, reloadOnChange: true)
        //    .AddEnvironmentVariables()
        //    .Build();


        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
