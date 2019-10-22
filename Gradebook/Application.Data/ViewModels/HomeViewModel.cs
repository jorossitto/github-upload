using System.Collections.Generic;

namespace Application.Data.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesofTheWeek { get; set; }
    }
}