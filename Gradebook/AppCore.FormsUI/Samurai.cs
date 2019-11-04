using AppCore.Data;
using System.ComponentModel;
using System.Windows.Forms;
using System;

namespace AppCore.FormsUI
{
    public partial class Samurai : Form
    {
        public Samurai()
        {
            InitializeComponent();
        }

        private readonly ConnectedData data = new ConnectedData();
        private Samurai currentSamurai;
        private bool isListChanging;
        private bool isLoading;
        private BindingList<Samurai> samuraiViewSource;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.isLoading = true;
            samuraiListBox.SelectedItem = data.SamuraisListInMemory();
            //this.samuraiViewSource = (BindingList<Samurai>)FindResource(config.samuraiViewSource);
            this.isLoading = false;
            samuraiListBox.SelectedIndex = 0;
        }
        private void samuraiListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                isListChanging = true;
                //currentSamurai = data.LoadSamuraiGraph((int)samuraiListBox.SelectedValue);
                samuraiViewSource.Add(currentSamurai);
                isListChanging = false;
            }
        }

            private void Window_Closing(object sender, CancelEventArgs e)
            {
                if (data.ChangesNeedToBeSaved())
                {
                    e.Cancel = PromptSaveChanges();
                }
            }

            private bool PromptSaveChanges()
            {
                string messageBoxText = config.UnsavedChanges;
                var result = MessageBox.Show(messageBoxText, "Samurai", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                bool cancelOperation = false;
                switch (result)
                {
                    case DialogResult.Yes:
                        data.SaveChanges();
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        cancelOperation = true;
                        break;
                }

                return cancelOperation;
            }

        private void NewSamurai_Click(object sender, EventArgs e)
        {
            //currentSamurai = data.CreateNewSamurai();
            //samuraiViewSource.ObjectInstance = currentSamurai;
            samuraiListBox.SelectedItem = currentSamurai;
        }

        private void SamuraiRealName_TextChanged(object sender, EventArgs e)
        {
            if (!isLoading && isListChanging)
            {
                //if (currentSamurai.sec == null)
                {

                }
            }
        }
    }
}
