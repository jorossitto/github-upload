using System;
using System.IO;
using System.Linq;
using Common;

namespace ACM.BL
{
    public class Pie : Product
    {
        public int PieId { get; set; }
        public string AllergyInformation { get; set; }
        public bool IsPieofTheWeek { get; set; }
        public bool IsPieOfTheWeek { get; internal set; }
    }
}
