using System.Collections.Generic;

namespace AppCore.Data.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesofTheWeek { get; set; }
    }
}