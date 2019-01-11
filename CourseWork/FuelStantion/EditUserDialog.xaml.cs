using System.Linq;
using System.Windows;

namespace FuelStantion
{
    /// <summary>
    /// Interaction logic for EditUserDialog.xaml
    /// </summary>
    public partial class EditUserDialog : Window
    {
        public EditUserDialog()
        {
            InitializeComponent();
            FillTypeCombobox();
        }

        private bool IsValidInput()
        {
            if (MyApplication.Instance.GetFuelStorageViewModel().DBConnection.Users.Where(u => u.Login == loginTextBox.Text).Count() != 0)
                return false;

            if (passwordBox.Password.Length == 0)
                return false;

            return true;
        }

        public void CopyValuesToUserData(User userData)
        {
            userData.Login = loginTextBox.Text;
            userData.PasswordHash = Utils.GetHashString(passwordBox.Password);
            userData.Type = (EUserType) TypeComboBox.SelectedValue;
        }

        class ComboboxUserTypeItem
        {
            public int Type
            {
                get
                {
                    return (int) UserType;
                }
                set
                {
                    UserType = (EUserType) value;
                }
            }
            public EUserType UserType { get; set; }
        };

        void FillTypeCombobox()
        { 
            TypeComboBox.DisplayMemberPath = "UserType";
            TypeComboBox.SelectedValuePath = "Type";

            TypeComboBox.Items.Add(new ComboboxUserTypeItem() { UserType = EUserType.Seller });
            TypeComboBox.Items.Add(new ComboboxUserTypeItem() { UserType = EUserType.Admin });
            TypeComboBox.SelectedIndex = 0;
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
