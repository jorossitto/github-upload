using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace AppCore.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BusinessDBContext dBContext;
        private readonly ShoppingCart shoppingCart;

        public OrderRepository(BusinessDBContext dBContext, ShoppingCart shoppingCart)
        {
            this.dBContext = dBContext;
            this.shoppingCart = shoppingCart;
        }
        
        public void CreateOrder(Order order)
        {
            Contract.Requires(order != null);
            order.OrderPlaced = DateTime.Now;
            var shoppingCartItems = shoppingCart.ShoppingCartItems;
            order.OrderTotal = shoppingCart.GetShoppingCartTotal();
            order.OrderDetails = new List<OrderDetail>();

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    Price = shoppingCartItem.Pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();
        }
    }
}