using System;
using System.Collections.Generic;

namespace ACM.BL
{
    public class Speaker : Person
    {
        public new object Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public List<Session> Sessions { get; set; }

        public static List<Speaker> testSpeakers()
        {
            var tempSpeakerList = new List<Speaker>();

            Speaker tempSpeaker = new Speaker
            {
                Id = Guid.Parse("TestData"),
                Name = "Test Name 1",
                Birthday = new DateTime(1987, 01, 29),
                Sessions = Session.testSessions()
            };
            tempSpeakerList.Add(tempSpeaker);

            tempSpeaker = new Speaker
            {
                Id = Guid.Parse("TestData"),
                Name = "Test Name 2",
                Birthday = new DateTime(1987, 01, 29),
                Sessions = Session.testSessions()

            };
            tempSpeakerList.Add(tempSpeaker);

            return tempSpeakerList;
        }

        internal object Include(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
