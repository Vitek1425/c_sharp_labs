using System.Windows;

namespace FuelStantion
{
    /// <summary>
    /// Interaction logic for EditUserTypeDialog.xaml
    /// </summary>
    public partial class EditUserTypeDialog : Window
    {
        public EditUserTypeDialog()
        {
            InitializeComponent();
            FillTypeCombobox();
        }

        public EUserType Type
        {
            get
            {
                return (EUserType)TypeComboBox.SelectedValue;
            }
            set
            {
                TypeComboBox.SelectedItem = value;
            }
        }

        class ComboboxUserTypeItem
        {
            public int Type
            {
                get
                {
                    return (int)UserType;
                }
                set
                {
                    UserType = (EUserType)value;
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
            this.DialogResult = true;
        }
    }
}
