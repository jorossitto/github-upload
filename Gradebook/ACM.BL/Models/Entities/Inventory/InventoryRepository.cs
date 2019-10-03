using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class InventoryRepository : EntityBase
    {

        public override bool Validate()
        {
            throw new NotImplementedException();
        }

        internal void OrderItems(Order order, bool allowSplitOrders)
        {
            //throw new NotImplementedException();
        }
    }
}
