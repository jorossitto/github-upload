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
    public partial class PedometerWin : Form
    {
        public PedometerWin()
        {
            InitializeComponent();
        }

        private void CalculateBTN_Click(object sender, EventArgs e)
        {
            var customer = new Customer();
            try
            {
                var result = 
                    MathExtentions.CalculatePercentOfGoalSteps(this.StepGoalTxt.Text,this.TotalStepsTxt.Text);
                ResultLabel.Text = $"You reached {result}% of your goal!";
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show("Your entry was not valid: " + exception.Message);
                ResultLabel.Text = string.Empty;
            }



            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
