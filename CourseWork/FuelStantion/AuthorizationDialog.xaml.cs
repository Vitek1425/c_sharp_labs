using System.Linq;
using System.Windows;

namespace FuelStantion
{
    /// <summary>
    /// Interaction logic for AuthorizationDialog.xaml
    /// </summary>
    public partial class AuthorizationDialog : Window
    {
        public AuthorizationDialog()
        {
            InitializeComponent();
        }

        private bool IsValidInput()
        {
            return User != null;
        }

        public User User
        {
            get
            {
                string inputPasswordHash = Utils.GetHashString(passwordBox.Password);

                var users = MyApplication.Instance.GetFuelStorageViewModel().DBConnection.Users.Where(
                    u => (
                    u.Login == loginTextBox.Text &&
                    u.PasswordHash == inputPasswordHash)
                    ).ToList();

                if (users.Count() == 1)
                    return users.First();
                else
                    return null;
            }
        }

        public string Password
        {
            get
            {
                return passwordBox.Password;
            }
            set
            {
                passwordBox.Password = value;
            }
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
