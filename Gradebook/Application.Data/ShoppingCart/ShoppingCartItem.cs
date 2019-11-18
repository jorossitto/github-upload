using ACM.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Data
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public virtual Pie Pie { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }

    }
}
