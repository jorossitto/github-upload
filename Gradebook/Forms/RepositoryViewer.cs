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
    public partial class RepositoryViewer : Form
    {
        public RepositoryViewer()
        {
            InitializeComponent();
            ClickMeBtn.Click += Observer1;
            ClickMeBtn.Click += Observer2;
            ClickMeBtn.Click += Observer3;
        }

        private void Observer1(object sender, EventArgs e)
        {
            MessageBox.Show("Hellow from observer 1");
        }
        private void Observer2(object sender, EventArgs e)
        {
            MessageBox.Show("Hellow from observer 2");
        }

        private void Observer3(object sender, EventArgs e)
        {
            MessageBox.Show("Hellow from observer 3");
        }

        private void ServiceBtn_Click(object sender, EventArgs e)
        {
            ClearListBox();
            IPersonRepository repository = new ServiceRepository();
            var people = repository.GetPeople();
            foreach (var person in people)
            {
                RepositoryTypeTextBlock.Items.Add(person);

            }
            ShowRepositoryType(repository);
        }

        private void ShowRepositoryType(IPersonRepository repository)
        {
            RepositoryTypeTextBlock.Text = repository.GetType().ToString();
        }

        private void ClearDataBtn_Click(object sender, EventArgs e)
        {
            ClearListBox();
            RepositoryTypeTextBlock.Text = string.Empty;
        }

        private void ClearListBox()
        {
            RepositoryTypeTextBlock.Items.Clear();
        }

        private void ClickMeBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
