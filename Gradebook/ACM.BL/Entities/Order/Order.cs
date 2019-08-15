using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class Order : EntityBase, ILoggable
    {
        public Order() : this(0)
        {
            
        }

        public Order(int orderId)
        {
            OrderId = orderId;
            OrderItems = new List<OrderItem>();
        }
        public int CustomerId { get; set; }
        public DateTimeOffset? OrderDate { get; set; }
        public int OrderId { get; private set; }
        public List<OrderItem> OrderItems { get; set; }
        public int ShippingAddressId { get; set; }

        public string Log() => $"{OrderId}: Date: {this.OrderDate.Value.Date} Status: {this.EntityState.ToString()}";

        public override string ToString() => 
            $"{OrderDate.Value.Date} ({OrderId})";

        public override bool Validate()
        {
            var isValid = true;
            if (OrderDate == null) isValid = false;

            return isValid;
        }
    }
}
