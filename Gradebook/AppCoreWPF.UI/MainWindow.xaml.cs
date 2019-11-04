using AppCore.Data;
using AppCore.Domain;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AppCoreWPF.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ConnectedData data = new ConnectedData();
        private Samurai currentSamurai;
        private bool isListChanging;
        private bool isLoading;
        private ObjectDataProvider samuraiViewSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.isLoading = true;
            samuraiListBox.ItemsSource = data.SamuraisListInMemory();
            this.samuraiViewSource = (ObjectDataProvider)FindResource(config.samuraiViewSource);
            this.isLoading = false;
            samuraiListBox.SelectedIndex = 0;
        }

        private void samuraiListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!isLoading)
            {
                isListChanging = true;
                currentSamurai = data.LoadSamuraiGraph((int)samuraiListBox.SelectedValue);
                samuraiViewSource.ObjectInstance = currentSamurai;
                isListChanging = false;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if(data.ChangesNeedToBeSaved())
            {
                e.Cancel = PromptSaveChanges();
            }
        }

        private bool PromptSaveChanges()
        {
            string messageBoxText = config.UnsavedChanges;
            var result = MessageBox.Show(messageBoxText, "Samurai", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            bool cancelOperation = false;
            switch(result)
            {
                case MessageBoxResult.Yes:
                    data.SaveChanges();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    cancelOperation = true;
                    break;
            }

            return cancelOperation;
        }

        private void QuotesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NewSamurai_Click(object sender, RoutedEventArgs e)
        {
            currentSamurai = data.CreateNewSamurai();
            samuraiViewSource.ObjectInstance = currentSamurai;
            samuraiListBox.SelectedItem = currentSamurai;
        }

        private void realNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!isLoading && isListChanging)
            {
                if(currentSamurai.SecretIdentity == null)
                {

                }
            }
        }
    }
}
