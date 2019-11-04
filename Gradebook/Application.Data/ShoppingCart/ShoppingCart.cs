﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCore.Data
{
    public class ShoppingCart
    {
        private readonly BusinessDBContext dBContext;

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCart(BusinessDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<BusinessDBContext>();



            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Pie pie, int amount)
        {
            var shoppingCartItem = dBContext.ShoppingCartItems.SingleOrDefault
                (
                    s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId
                );

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };

                dBContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            dBContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem = dBContext.ShoppingCartItems.SingleOrDefault
                (
                    s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId
                );
            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    dBContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            dBContext.SaveChanges();
            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                (ShoppingCartItems = dBContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId).Include(s => s.Pie).ToList());
        }

        public void ClearCart()
        {
            var cartItems = dBContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);

            dBContext.ShoppingCartItems.RemoveRange(cartItems);

            dBContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = dBContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }
    }
}
