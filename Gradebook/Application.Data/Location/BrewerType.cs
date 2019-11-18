using System;

namespace AppCore.Data
{
    public class BrewerType
    {
        public int BrewerTypeId { get; set; }
        public string Description { get; set; }
        
        public virtual Recipe Recipe { get; set; }

        public System.Drawing.Color Color { get; set; }

    }
}