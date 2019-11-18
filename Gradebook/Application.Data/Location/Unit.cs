using System;

namespace AppCore.Data
{
    public class Unit
    {
        public int UnitId { get; set; }
        public int? LocationId { get; set; }
        public int BrewerTypeId { get; set; }
        public DateTime Acquired { get; set; }

    }
}