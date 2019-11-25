using System;

namespace AppCore.Entities
{
    public class Milestone
    {
        public Milestone(int locationId, DateTime date, string description)
        {
            Description = description;
            Date = date;
            LocationId = locationId;
        }

        public int MilestoneId { get; set; }
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

    }
}