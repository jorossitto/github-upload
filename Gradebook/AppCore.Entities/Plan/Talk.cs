using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppCore.Entities
{
    public class Talk
    {

        public int PresenterId { get; private set; }
        public Presenter Presenter { get; private set; }

        public int TalkId { get; set; }
        public virtual Camp Camp { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Abstract { get; set; }
        public int Level { get; set; }
        public virtual Speaker Speaker { get; set; }
        #region Submissions
        private List<Submission> _submission = new List<Submission>();
        public IEnumerable<Submission> Submissions => _submission;
        public Submission Submit(int conferenceId)
        {
            var submission = new Submission
            {
                Talk = this,
                ConferenceId = conferenceId
            };
            _submission.Add(submission);
            return submission;
        }
        #endregion
    }
}