using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public sealed class Mediator
    {
        //Static Members
        private static readonly Mediator _Instance = new Mediator();
        private Mediator() { }

        public static Mediator GetInstance()
        {
            return _Instance;
        }

        //Instance functionality
        public event EventHandler<JobChangedEventArgs> JobChanged;

        public void OnJobChanged(object sender, Job job)
        {
            var jobChangedDelegate = JobChanged as EventHandler<JobChangedEventArgs>;

            if(jobChangedDelegate != null)
            {
                jobChangedDelegate(sender, new JobChangedEventArgs { Job = job });
            }
        }
    }
}
