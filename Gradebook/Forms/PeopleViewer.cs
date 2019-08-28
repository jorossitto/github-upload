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
    public partial class PeopleViewer : Form
    {
        PersonRepository repository = new PersonRepository();

        public PeopleViewer()
        {
            InitializeComponent();
        }

        private void ConcreteTypeBtn_Click(object sender, EventArgs e)
        {
            ClearListBox();
            IEnumerable<Person> people = repository.GetPeople();
            foreach (var person in people)
            {
                PersonListBox.Items.Add(person);
            }
        }

        private void ClearDataBtn_Click(object sender, EventArgs e)
        {
            ClearListBox();
        }

        private void ClearListBox()
        {
            PersonListBox.Items.Clear();
        }

        private void AbstractionBtn_Click(object sender, EventArgs e)
        {
            ClearListBox();
            IEnumerable<Person> people = repository.GetPeople();
            foreach (var person in people)
            {
                PersonListBox.Items.Add(person);
            }
        }

        private void RepositoryMenuBtn_Click(object sender, EventArgs e)
        {
            var form = new RepositoryViewer();
            form.Show();
        }
    }
}
