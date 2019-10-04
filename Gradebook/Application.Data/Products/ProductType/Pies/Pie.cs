using System.IO;
using System.Linq;
using Common;
using ACM.BL;

namespace Application.Data
{
    public class Pie : Product
    {
        public int PieId { get; set; }
        public string AllergyInformation { get; set; }
        public bool IsPieOfTheWeek { get; set; }
    }
}
