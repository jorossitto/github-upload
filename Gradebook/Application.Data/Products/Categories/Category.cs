using System.Collections.Generic;

namespace AppCore.Data
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public virtual List<Pie> Pies { get; set; }
    }
}