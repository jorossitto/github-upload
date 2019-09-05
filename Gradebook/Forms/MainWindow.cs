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

        private void CountryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Current index " + CountryBox.SelectedIndex.ToString());
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            AssignWork();

            BizRulesDelegate addDel = (x, y) => x + y;
            BizRulesDelegate multiplyDel = (x, y) => x * y;
            var data = new ProcessData();
            data.Process(2, 3, addDel);

            Action<int, int> myAction = (x, y) => Console.WriteLine(x+y);
            data.ProcessAction(2, 3, myAction);

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
    }
}
