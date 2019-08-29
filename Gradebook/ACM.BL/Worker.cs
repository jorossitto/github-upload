using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public enum WorkType
    {
        GoToMeetings,
        Golf,
        GenerateReports
    }

    public delegate int WorkPerformedHandler(int hours, WorkType workType);

    public class Worker
    {
        public event WorkPerformedHandler WorkPerformed;
        public event EventHandler WorkCompleted;

        public void DoWork(int hours, WorkType workType)
        {
            for(int i = 0; i < hours; i ++)
            {
                OnWorkPerformed(i + 1, workType);
                
            }

            OnWorkCompleted();
            //Raise Event
        }

        protected virtual void OnWorkPerformed(int hours, WorkType workType)
        {
            //WorkPerformed?.Invoke(hours, workType);
            (WorkPerformed as WorkPerformedHandler)?.Invoke(hours, workType);
        }

        protected virtual void OnWorkCompleted()
        {
            //OnWorkCompleted?.Invoke(this, EventArgs.Empty);
            (WorkCompleted as EventHandler)?.Invoke(this, EventArgs.Empty);
        }
    }
}
