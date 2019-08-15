using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class FakeSpeakerRepository : ISpeakerRepository
    {
        public IEnumerable<Speaker> Speakers { get; } = Speaker.testSpeakers();

        public SessionBuilderContext context => throw new NotImplementedException();

        public Speaker Get(string name)
        {
            throw new NotImplementedException();
        }

        public Speaker Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
