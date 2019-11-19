using System;

namespace AppCore.Data
{
    public class UnitQueryType
    {
        public string Address1 { get; private set; }
        public string BrewerTypeDescription { get; private set; }
        public DateTime Acquired { get; private set; }
        public decimal Cost { get; private set; }
    }
}