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
    public partial class DateRangeDialog : Window
    {
        public DateRangeDialog()
        {
            InitializeComponent();
        }

        private bool IsValidDates()
        {
            return beginDatePicker.SelectedDate != null &&
                   endDatePicker.SelectedDate != null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidDates())
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Wrong input dates", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public long BeginUnixDate()
        {
            DateTimeOffset timeOffet = new DateTimeOffset(beginDatePicker.SelectedDate ?? DateTime.Now);
            return timeOffet.ToUnixTimeSeconds();
        }

        public long EndUnixDate()
        {
            DateTimeOffset timeOffet = new DateTimeOffset(endDatePicker.SelectedDate ?? DateTime.Now);
            return timeOffet.ToUnixTimeSeconds();
        }
    }
}
