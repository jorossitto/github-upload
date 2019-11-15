using System.Collections.Generic;

namespace AppCore.Data
{
    public class Location
    {
        public Location()
        {
            BrewingUnits = new List<Unit>();
            Milestones = new List<Milestone>();
        }

        public Location(string address1, string openTime, string closeTime) : this()
        {
            Address1 = address1;
            OpenTime = openTime;
            CloseTime = closeTime;
        }

        public int LocationId { get; private set; }
        public string VenueName { get; set; }
        public string Address1 { get; private set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string CityTown { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string OpenTime { get; private set; }
        public string CloseTime { get; private set; }
        public List<Unit> BrewingUnits { get; set; }
        public List<Milestone> Milestones { get; set; }
    }
}