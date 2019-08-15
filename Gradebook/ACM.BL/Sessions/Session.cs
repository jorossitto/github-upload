using System;
using System.Collections.Generic;

namespace ACM.BL
{
    public class Session
    {
        public object Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public DateTimeOffset ScheduledAt { get; set; }
        public DateTimeOffset SubmittedAt { get; set; }
        public TimeSpan Length { get; set; }

        public static List<Session> testSessions()
        {
            var testSessionList = new List<Session>();

            Session testSession = new Session
            {
                Id = Guid.Parse("TestData"),
                Title = "Test Title 0",
                Abstract = "Test Abstract 1",
                Length = TimeSpan.FromMinutes(120),
                SubmittedAt = new DateTimeOffset(2016, 02, 29, 00, 01, 00, TimeSpan.FromHours(1)),
                ScheduledAt = new DateTimeOffset(2019, 08, 01, 09, 40, 00, TimeSpan.FromHours(2))

            };
            testSessionList.Add(testSession);

            testSession = new Session
            {
                Id = Guid.Parse("TestData"),
                Title = "Test Title 1",
                Abstract = "Test Abstract 1",
                Length = TimeSpan.FromMinutes(120),
                SubmittedAt = new DateTimeOffset(2016, 02, 29, 1, 01, 00, TimeSpan.FromHours(1)),
                ScheduledAt = new DateTimeOffset(2019, 08, 01, 09, 40, 00, TimeSpan.FromHours(2))

            };
            testSessionList.Add(testSession);

            testSession = new Session
            {
                Id = Guid.Parse("TestData"),
                Title = "Test Title 2",
                Abstract = "Test Abstract 1",
                Length = TimeSpan.FromMinutes(120),
                SubmittedAt = new DateTimeOffset(2016, 02, 28, 23, 01, 00, TimeSpan.FromHours(1)),
                ScheduledAt = new DateTimeOffset(2019, 08, 01, 09, 40, 00, TimeSpan.FromHours(2))

            };
            testSessionList.Add(testSession);

            testSession = new Session
            {
                Id = Guid.Parse("TestData"),
                Title = "Test Title 3",
                Abstract = "Test Abstract 1",
                Length = TimeSpan.FromMinutes(120),
                SubmittedAt = new DateTimeOffset(2016, 02, 28, 20, 01, 00, TimeSpan.FromHours(1)),
                ScheduledAt = new DateTimeOffset(2019, 08, 01, 09, 40, 00, TimeSpan.FromHours(2))

            };
            testSessionList.Add(testSession);

            testSession = new Session
            {
                Id = Guid.Parse("TestData"),
                Title = "Test Title 4",
                Abstract = "Test Abstract 1",
                Length = TimeSpan.FromMinutes(120),
                SubmittedAt = new DateTimeOffset(2016, 02, 29, 4, 01, 00, TimeSpan.FromHours(1)),
                ScheduledAt = new DateTimeOffset(2019, 08, 01, 09, 40, 00, TimeSpan.FromHours(2))

            };
            testSessionList.Add(testSession);

            return testSessionList;
        }
    }
}
