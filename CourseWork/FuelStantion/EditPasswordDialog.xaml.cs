using System.Windows;

namespace FuelStantion
{
    /// <summary>
    /// Interaction logic for EditPassword.xaml
    /// </summary>
    public partial class EditPasswordDialog : Window
    {
        public EditPasswordDialog()
        {
            InitializeComponent();
        }

        public string Password
        {
            get
            {
                return passwordBox.Password;
            }
        }

        bool IsValidInput()
        {
            return Password.Length != 0;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInput())
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Wrong input data", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
