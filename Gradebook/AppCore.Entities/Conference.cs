using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppCore.Entities
{
    public class Conference
    {
        private List<Speaker> _speakers = new List<Speaker>();

        public IEnumerable<Speaker> Speakers => _speakers;

        public int ConferenceId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Identity { get; set; }
        public int Identifier { get; set; }
        public string Name { get; set; }
        public void AddSpeaker()
        {

        }

    }
}