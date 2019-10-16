using System.Collections.Generic;
using Application.Data;

namespace BusinessSample.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesofTheWeek { get; set; }
    }
}