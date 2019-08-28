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
        WorkPerformedHandler del1 = new WorkPerformedHandler(WorkPerformed1);
        WorkPerformedHandler del2 = new WorkPerformedHandler(WorkPerformed2);
        WorkPerformedHandler del3 = new WorkPerformedHandler(WorkPerformed3);



        public MainWindow()
        {
            InitializeComponent();

            del1(5, WorkType.Golf);
            del2(10, WorkType.GenerateReports);

            del1 += del2 + del3;
            
            
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
            //MessageBox.Show("Clicked Me");
            int finalhours  = del1(10, WorkType.GenerateReports);
            Console.WriteLine(finalhours);
        }
        static void DoWork(WorkPerformedHandler del)
        {
            del(5, WorkType.GoToMeetings);   
        }
        static int WorkPerformed1(int hours, WorkType workType)
        {
            Console.WriteLine($"WorkPerformed1 called: {hours}, {workType}");
            return hours + 1;
        }
        static int WorkPerformed2(int hours, WorkType workType)
        {
            Console.WriteLine($"WorkPerformed2 called: {hours}, {workType}");
            return hours + 2;
        }

        static int WorkPerformed3(int hours, WorkType workType)
        {
            Console.WriteLine($"WorkPerformed3 called: {hours}, {workType}");
            return hours + 3;
        }
    }
}
