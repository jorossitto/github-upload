using Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACM.BL
{


    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PeopleMenuBtn_Click(object sender, EventArgs e)
        {
            var form = new PeopleViewer();
            form.Show();
        }

        private void jobMenuBtn_Click(object sender, EventArgs e)
        {
            var form = new JobsForm();
            form.Show();
        }
        private void CountryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Current index " + CountryBox.SelectedIndex.ToString());
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            AssignWork();
            CreateAndDisplayBusinessRules();
            CreateAndDisplayCustomers();
        }

        private static void CreateAndDisplayBusinessRules()
        {
            //var data = new ProcessData();

            BizRulesDelegate addDel = (x, y) => x + y;
            BizRulesDelegate multiplyDel = (x, y) => x * y;
            ProcessData.Process(2, 3, addDel);

            Action<int, int> myAction = (x, y) => Console.WriteLine(x + y);
            ProcessData.ProcessAction(2, 3, myAction);

            Func<int, int, int> funcAddDel = (x, y) => x + y;
            Func<int, int, int> funcMultDel = (x, y) => x * y;
            ProcessData.ProcessFunc(2, 3, funcAddDel);
        }

        private static void CreateAndDisplayCustomers(int amount = 4)
        {
            var customerRepository = new CustomerRepository();
            IEnumerable<Customer> customers = new List<Customer>();
            customers = customerRepository.MakeDefaultCustomers(amount);

            var customerList = customers.OrderBy(c => c.FirstName);//Where(c => c.FirstName == "Test1");
            foreach (var customer in customerList)
            {
                Console.WriteLine($"First Name : {customer.FirstName}, Last Name: {customer.LastName}");
            }
        }

        private void AssignWork()
        {
            var worker = new Worker();
            worker.WorkPerformed += Worker_WorkPerformed;
            worker.WorkCompleted += Worker_WorkCompleted;
            worker.DoWork(8, WorkType.GenerateReports);
        }

        private void Worker_WorkPerformed(object sender, WorkPerfromedEventArgs e)
        {
            Console.WriteLine("Hours Worked: " + e.Hours + " " + e.WorkType);
        }

        private void Worker_WorkCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("Worker is done");
        }

        private void OrderBtn_Click(object sender, EventArgs e)
        {
            var form = new OrderWin();
            form.Show();
        }
    }
}
