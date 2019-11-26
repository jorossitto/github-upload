using System;

namespace AppCore.Entities
{
    public class Submission
    {
        public int SubmissionId { get; private set; }
        public DateTime DateSubmitted { get; set; }
        public int TalkId { get; private set; }
        public Talk Talk { get; internal set; }
        public int ConferenceId { get; internal set; }
        //public Conference Conference { get; internal set; }
        public bool Accepted { get; set; }
        public DateTime DateAccepted { get; set; }
    }
}