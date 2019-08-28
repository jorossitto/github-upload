using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public delegate int WorkPerformedHandler(int hours, WorkType workType);
    public enum WorkType
    {
        GoToMeetings,
        Golf,
        GenerateReports
    }
    public class Worker
    {
        public event WorkPerformedHandler WorkPerformed;
        public event EventHandler WorkCompleted;

        public void DoWork(int hours, WorkType workType)
        {

        }
    }
}
