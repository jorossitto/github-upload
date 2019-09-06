using ACM.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class JobsForm : Form
    {
        IEnumerable<Job> jobs = Job.MakeDefaultJobs();

        public JobsForm()
        {
            InitializeComponent();
            BindData();
        }

        private void BindData()
        {
            jobCombo.Items.Add(jobs).ToString();
        }

        private void jobCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mediator.GetInstance().OnJobChanged(this, (Job)jobCombo.SelectedItem);
        }
    }
}
