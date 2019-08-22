using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class OrderController : EntityBase
    {
        private CustomerRepository customerRepository { get; set; }
        private OrderRepository orderRepository { get; set; }
        private InventoryRepository inventoryRepository { get; set; }
        private EmailLibrary emailLibrary { get; set; }

        public OrderController()
        {
            var customerRepository = new CustomerRepository();
            var orderRepository = new OrderRepository();
            var inventoryRepository = new InventoryRepository();
            var emailLibrary = new EmailLibrary();


        }
        public void PlaceOrder(Customer customer, 
                                Order order,
                                Payment payment,
                                bool allowSplitOrders,
                                bool emailReceipt)
        {
            customerRepository.Add(customer);
            orderRepository.Add(order);
            inventoryRepository.OrderItems(order, allowSplitOrders);
            payment.ProcessPayment();

            if (emailReceipt)
            {
                customer.ValidateEmail();
                customerRepository.Update();

                
                emailLibrary.SendEmail(customer.EmailAddress, "Here is your reciept");
            }
        }
        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
