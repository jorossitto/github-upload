using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace ACM.BL
{
    public partial class OrderWin : Form
    {
        public OrderWin()
        {
            InitializeComponent();
        }

        private void OrderWin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.Text = "Processing ...";
            }
            
            PlaceOrder();
        }

        private void PlaceOrder()
        {
            var customer = new Customer();
            //Todo: Populate Customer Instance from on screen forms - Not yet implemented

            var order = new Order();
            //Todo: Populate Order Instance from on screen forms - Not Yet Implemented

            var payment = new Payment(); //Todo populate from ui

            var allowSplitOrders = true; //Todo Pull from ui

            var emailReceipt = true; //Todo pull from ui

            var orderController = new OrderController();
            try
            {
                orderController.PlaceOrder(customer, order, payment, allowSplitOrders, emailReceipt);
            }
            catch(ArgumentNullException exception)
            {
                // log the issue
                //display a message to the user that the order was not successful
            }
            
        }

        

        private void PlaceOrders_Click(object sender, EventArgs e)
        {
            var customer1 = new Customer();
            customer1.FirstName = "Mark";
            var customer2 = new Customer();
            customer2.FirstName = "Ramdevi";
            var orders = new ConcurrentQueue<string>();
            //OrderController.PlaceOrders(orders, customer1);
            //OrderController.PlaceOrders(orders, customer2);

            Task task1 = Task.Run(() => OrderController.PlaceOrders(orders, customer1));
            Task task2 = Task.Run(() => OrderController.PlaceOrders(orders, customer2));
            Task.WaitAll(task1, task2);

            foreach (var order in orders)
            {
                OrderController.ProcessOrder(order);
            }

            Parallel.ForEach(orders, OrderController.ProcessOrder);
            foreach (var order in orders)
            {
                Console.WriteLine("Order: " + order);
            }

        }

        private void StockBtn_Click(object sender, EventArgs e)
        {
            bool success;
            var stock = new ConcurrentDictionary<string, int>();
            stock.TryAdd("jDays", 4);
            stock.TryAdd("technologyhour", 3);

            Console.WriteLine(string.Format("No. of shirts in stock = {0}", stock.Count));

            stock.TryAdd("pluralsight", 6);
            stock["buddhistgeeks"] = 5;

            int psStock = stock.AddOrUpdate("pluralsight", 1, (key, oldValue) 
                => oldValue + 1);

            Console.WriteLine($"New value is {psStock}");

            Console.WriteLine(string.Format("stock[pluralsight] = {0}", 
                stock.GetOrAdd("pluralsight", 0)));

            int jDaysValue;
            success = stock.TryRemove("jDays", out jDaysValue);

            Console.WriteLine("\r\nEumerating:");
            foreach (var keyValPair in stock)
            {
                Console.WriteLine("{0}: {1}", keyValPair.Key, keyValPair.Value);
            }
        }
    }
}
