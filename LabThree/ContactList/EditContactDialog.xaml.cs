using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ContactList
{
    public partial class EditContactDialog : Window
    {
        public EditContactDialog()
        {
            InitializeComponent();
        }    

        private bool IsValidInput()
        {
            return nameTextBox.Text != "";
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

        public Contact GetContact()
        {
            if (!IsValidInput())
                return null;

            DateTimeOffset timeOffet = new DateTimeOffset(birthDatePicker.SelectedDate ?? DateTime.Now);

            Contact contact = new Contact
            {
                Name = nameTextBox.Text,
                WorkPhone = workPhoneTextBox.Text,
                HomePhone = homePhoneTextBox.Text,
                Email = emailTextBox.Text,
                BirthDate = timeOffet.ToUnixTimeSeconds(),
                Comment = commentTextBox.Text
            };

            return contact;
        }

        public void SetContact(Contact contact)
        {
            if (contact == null)
                return;

            DateTimeOffset timeOffet = DateTimeOffset.FromUnixTimeSeconds(contact.BirthDate);
            birthDatePicker.SelectedDate = timeOffet.UtcDateTime;

            nameTextBox.Text = contact.Name;
            workPhoneTextBox.Text = contact.WorkPhone;
            homePhoneTextBox.Text = contact.HomePhone;
            emailTextBox.Text = contact.Email;
            commentTextBox.Text = contact.Comment;
        }
    }
}
