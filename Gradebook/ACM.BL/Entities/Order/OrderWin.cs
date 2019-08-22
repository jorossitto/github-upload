using System;
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
            PlaceOrder();
        }

        private void PlaceOrder()
        {
            var customer = new Customer();
            //Todo: Populate Customer Instance from on screen forms - Not yet implemented

            var order = new Order();
            //Todo: Populate Order Instance from on screen forms - Not Yet Implemented

            var allowSplitOrders = true; //Todo Pull from ui

            var emailReceipt = true; //Todo pull from ui

            var payment = new Payment(); //Todo populate from ui

            var orderController = new OrderController();
            orderController.PlaceOrder(customer, order, payment, allowSplitOrders, emailReceipt);
        }
    }
}
