using System;

namespace AppCore.Data
{
    public class Unit
    {
        public Unit()
        {
            //Location = new Location();
            //BrewerType = new BrewerType();
        }

        public int UnitId { get; set; }
        public int? LocationId { get; set; }
        //public Location Location { get; set; }
        public int BrewerTypeId { get; set; }
        //public BrewerType BrewerType { get; set; }
        public DateTime Acquired { get; set; }
        public decimal Cost { get; set; }
        public DateTime? OutOfService { get; set; }


    }
}