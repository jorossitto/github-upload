using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACM.BL;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SessionBuilder.CoreWeb
{
    public class DownloadController : Controller
    {
        private readonly ISpeakerRepository speakerRepository;

        public DownloadController(ISpeakerRepository speakerRepository)
        {

        }
        public FileContentResult Index(Guid id)
        {
            var speaker = speakerRepository.Get(id);
            var csv = "Title,Speaker,Length,ScheduledAt" + Environment.NewLine;

            return File(Encoding.UTF8.GetBytes(csv), "text/csv", $"sessions for {speaker.Name}.csv");

        }
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
