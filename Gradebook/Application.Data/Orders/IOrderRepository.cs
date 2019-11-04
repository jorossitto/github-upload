using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Data
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
