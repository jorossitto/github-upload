using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Makes (amount) of default customers
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static IEnumerable<Job> MakeDefaultJobs(int amount = 1)
        {
            List<Job> jobs = new List<Job>();

            for (int i = 0; i < amount; i++)
            {
                Job job = new Job();
                job.Id = i;
                job.Title = "Title" + i;
                job.StartDate = DateTime.Now;
                job.EndDate = DateTime.Now.AddDays(4);
                jobs.Add(job);
            }
            return jobs;
        }
    }
}
