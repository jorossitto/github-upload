﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ACM.BL;
using TestNinja.Fundamentals;

namespace SessionBuilder.CoreWeb.Pages
{
    public enum DateComparison
    {
        Earlier = -1,
        Later = 1,
        TheSame = 0
    };

    public class IndexModel : PageModel
    {
        private readonly ISpeakerRepository repository;
        public Speaker Speaker { get; set; }
        public IndexModel(ISpeakerRepository repository)
        {

        }
        public void OnGet()
        {

        }
        public bool DoesSpeakerHaveEarlierSessions(Session currentSession)
        {
            return Speaker.Sessions.Any(session => session.Id != currentSession.Id 
            && session.ScheduledAt.CompareTo(currentSession.ScheduledAt) == (int)DateComparison.Earlier);
        }
        public bool DoesSpeakerHaveLaterSessions(Session currentSession)
        {
            return Speaker.Sessions.Any(session => session.Id != currentSession.Id 
            && session.ScheduledAt.CompareTo(currentSession.ScheduledAt) == (int)DateComparison.Later);
        }
        public bool IsSessionOverLapping(Session currentSession)
        {
            foreach(var session in Speaker.Sessions)
            {
                if (session.Id == currentSession.Id)
                {
                    continue;
                }

                if(session.ScheduledAt.IsBetween(currentSession.ScheduledAt, 
                    currentSession.ScheduledAt.Add(currentSession.Length)))
                {
                    return true;
                }
                else if (currentSession.ScheduledAt.IsBetween(session.ScheduledAt, 
                    session.ScheduledAt.Add(session.Length)))
                {
                    return true;
                }
            }
            return false;
        }
        public TimeSpan GetTimeUntilNextSession(Session currentSession)
        {
            var nextSession = Speaker.Sessions.OrderBy(session => session.ScheduledAt)
                .FirstOrDefault(session => session.Id != currentSession.Id
                && session.ScheduledAt >= currentSession.ScheduledAt);

            if (nextSession == null) return TimeSpan.MinValue;

            return nextSession.ScheduledAt - currentSession.ScheduledAt.Add(currentSession.Length);
        }
        public TimeSpan TimeSinceSubmission(Session session)
        {
            var timeSinceSubmission = 
                session.SubmittedAt - DateTimeOffset.UtcNow.ToOffset(session.SubmittedAt.Offset);
            return timeSinceSubmission;
        }

        public int GetSpeakerAge()
        {
            var today = DateTime.UtcNow.Date;
            var age = today.Year - Speaker.Birthday.Year;

            if(Speaker.Birthday.Date > today.Date.AddYears(-age))
            {
                age -= 1;
            }

            return age;
        }

        public int GetDaysUntilNextBirthday()
        {
            var today = DateTime.UtcNow.Date;
            var birthday = new DateTime(today.Year, Speaker.Birthday.Month, 1);
            birthday = birthday.AddDays(Speaker.Birthday.Day - 1);

            if(birthday < today)
            {
                birthday = new DateTime(today.Year + 1, Speaker.Birthday.Month, 1);
                birthday = birthday.AddDays(Speaker.Birthday.Day - 1);
            }
            return (int)(birthday - today).TotalDays;
        }


    }
}
