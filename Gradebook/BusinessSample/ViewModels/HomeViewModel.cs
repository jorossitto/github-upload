using System.Collections.Generic;
using Application.Data;

namespace BusinessSample.Controllers
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesofTheWeek { get; set; }
    }
}