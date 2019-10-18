using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
