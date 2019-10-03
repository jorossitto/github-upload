using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class OrderController : EntityBase
    {
        public CustomerRepository customerRepository { get; private set; }
        public OrderRepository orderRepository { get; private set; }
        public InventoryRepository inventoryRepository { get; private set; }
        public EmailLibrary emailLibrary { get; private set; }

        public OrderController()
        {
             customerRepository = new CustomerRepository();
             orderRepository = new OrderRepository();
             inventoryRepository = new InventoryRepository();
             emailLibrary = new EmailLibrary();
        }
        /// <summary>
        /// Places a customer order
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="order"></param>
        /// <param name="payment"></param>
        /// <param name="allowSplitOrders"></param>
        /// <param name="emailReceipt"></param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Customer customer, 
                                Order order,
                                Payment payment,
                                bool allowSplitOrders,
                                bool emailReceipt)
        {
            Debug.Assert(customerRepository != null, "Missing customer repository instance");
            Debug.Assert(orderRepository != null, "Missing order repository instance");
            Debug.Assert(inventoryRepository != null, "Missing inventory repository instance");
            Debug.Assert(emailLibrary != null, "Missing email library instance");

            if (customer == null) throw new ArgumentNullException("Customer instance is null");
            if (order == null) throw new ArgumentNullException("Order instance is null");
            if (payment == null) throw new ArgumentNullException("Payment instance is null");

            var operationResult = new OperationResult();

            customerRepository.Add(customer);
            orderRepository.Add(order);
            inventoryRepository.OrderItems(order, allowSplitOrders);
            payment.ProcessPayment();

            if (emailReceipt)
            {
                var result = customer.ValidateEmail();
                if(result.Success == true)
                {
                    customerRepository.Update();
                    emailLibrary.SendEmail(customer.EmailAddress, "Here is your reciept");
                }
                else
                {
                    //log the message
                    if(result.MessageList.Any())
                    {
                        operationResult.AddMessage(result.MessageList[0]);
                    }
                }

            }
            return operationResult;
        }
        static object _lockObj = new object();
        public static void PlaceOrders(ConcurrentQueue<string> orders, Customer customer)
        {
            for(int i = 0; i < 5; i++)
            {
                Thread.Sleep(1);
                string orderName = string.Format("{0} wants t-shirt {1}",
                    customer.FullName, i + 1);
                lock(_lockObj)
                {
                    orders.Enqueue(orderName);
                }
                
            }
        }


        public static void ProcessOrder(string order)
        {

        }
        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
