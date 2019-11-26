using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppCore.Entities
{
    public class Session
    {
        public int SessionId { get; set; }
        public Guid SessionGuid { get; set; }
        public int ConferenceId { get; set; }
        public Conference Conference { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Abstract { get; set; }

        [Required]
        [MaxLength(50)]
        public string Speaker { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public ICollection<SessionTag> SessionTags { get; set; }

        public int ReachId { get; set; }
        public Reach Reach { get; set; }

    }
}
