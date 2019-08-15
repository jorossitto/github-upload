using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class SpeakerRepository : ISpeakerRepository
    {
        public readonly SessionBuilderContext context;

        public SpeakerRepository(SessionBuilderContext context)
        {
            this.context = context;
        }

        public Speaker Get(string name)
        {
            var speaker = new Speaker();
            //speaker.Sessions
            //return context.Speakers.Include(speaker => speaker.Sessions).FirstOrDefault(speaker => speaker.Name == name);
            return speaker;
        }

        public Speaker Get(Guid id)
        {
            var tempSpeaker = new Speaker();
            return tempSpeaker;
        }
    }
}
