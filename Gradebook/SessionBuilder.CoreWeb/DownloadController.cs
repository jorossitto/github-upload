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

            var offset = TimeSpan.FromHours(-11);

            foreach(var session in speaker.Sessions)
            {
                csv += $"{session.Title}, {speaker.Name}, {session.Length}, " +
                    $"{session.ScheduledAt.ToOffset(offset).ToString("o")}{Environment.NewLine}";
            }

            return File(Encoding.UTF8.GetBytes(csv), "text/csv", $"sessions for {speaker.Name}.csv");

        }
    }
}
