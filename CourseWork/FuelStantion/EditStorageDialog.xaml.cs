using System.Windows;

namespace FuelStantion
{
    /// <summary>
    /// Interaction logic for EditStorageDialog.xaml
    /// </summary>
    public partial class EditStorageDialog : Window
    {
        public EditStorageDialog()
        {
            InitializeComponent();
        }

        public string FuelName
        {
            set
            {
                fuelTextBox.Text = value;
            }
        }

        public double Volume {
            get
            {
                return (double)volumeSpinBox.Value;
            }
            set
            {
                if (value < 0)
                    value = 0;
                volumeSpinBox.Value = (decimal)value;
            }
        }

        public double Price
        {
            get
            {
                return (double)priceSpinBox.Value;
            }
            set
            {
                if (value < 0)
                    value = 0;
                priceSpinBox.Value = (decimal)value;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
