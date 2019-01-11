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

namespace FuelStantion
{
    /// <summary>
    /// Interaction logic for AddRefillingDialog.xaml
    /// </summary>
    public partial class AddRefillingDialog : Window
    {
        private double _selectedFuelPrice = 0.0;
        private double _selectedFuelMaxVolume = 0.0;
        public AddRefillingDialog()
        {
            InitializeComponent();
            FillComboBox();
            datePicker.SelectedDate = DateTime.Now;
        }

        private bool _isEditMode = false;
        public bool IsEditMode
        {
            get
            {
                return _isEditMode;
            }
            set
            {
                _isEditMode = value;
                operatorTextBox.IsReadOnly = !_isEditMode;
                priceSpinBox.IsReadOnly = !_isEditMode;
            }
        }


        public string OperatorName
        {
            get
            {
                return operatorTextBox.Text;
            }
            set
            {
                operatorTextBox.Text = value;
            }
        }
        
        public void CopyValuesFromFuelRefilling(FuelRefilling fuelRefilling)
        {
            operatorTextBox.Text = fuelRefilling.OperatorName;
            DateTimeOffset timeOffet = DateTimeOffset.FromUnixTimeSeconds(fuelRefilling.Date);
            datePicker.SelectedDate = timeOffet.UtcDateTime;
            fuelComboBox.SelectedValue = fuelRefilling.Fuel_type_id;
            volumeSpinBox.Value = (decimal) fuelRefilling.Volume;
            priceSpinBox.Value = (decimal)fuelRefilling.Price;

        }
        public void CopyValuesToFuelRefilling(FuelRefilling fuelRefilling)
        {
            fuelRefilling.OperatorName = operatorTextBox.Text;
            DateTimeOffset timeOffet = new DateTimeOffset(datePicker.SelectedDate ?? DateTime.Now);
            fuelRefilling.Date = timeOffet.ToUnixTimeSeconds();
            fuelRefilling.Fuel_type_id = (int)fuelComboBox.SelectedValue;
            fuelRefilling.Volume = (double)volumeSpinBox.Value;
            fuelRefilling.Price = GetPrice();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void FillComboBox()
        {
            fuelComboBox.ItemsSource = MyApplication.Instance.GetFuelStorageViewModel().FuelTypes;
            fuelComboBox.DisplayMemberPath = "Name";
            fuelComboBox.SelectedValuePath = "id";
            fuelComboBox.SelectedValue = 0;
            LoadFuelData();
        }


        private void OnPriceChanged()
        {
            if (priceSpinBox == null || IsEditMode)
                return;

            priceSpinBox.Value = (decimal) GetPrice();
        }

        private double GetPrice()
        {
            return ((double)volumeSpinBox.Value * _selectedFuelPrice);
        }

        private void On_VolumeChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if ((double)volumeSpinBox.Value > _selectedFuelMaxVolume)
            {
                MessageBox.Show("Not enough fuel", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                volumeSpinBox.Value = (decimal) _selectedFuelMaxVolume;
            }

            OnPriceChanged();
        }

        private void OnFuelChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadFuelData();
            OnPriceChanged();
        }

        private void LoadFuelData()
        {
            if (fuelComboBox.SelectedValue == null)
                return;
            int selectedFuel = (int)fuelComboBox.SelectedValue;

            var fuelStorage = MyApplication.Instance.GetFuelStorageViewModel().DBConnection.FuelStorages.Find(selectedFuel);
            if (fuelStorage == null)
            {
                _selectedFuelPrice = 0.0;
                _selectedFuelMaxVolume = 0.0;
            }
            else
            {
                _selectedFuelPrice = fuelStorage.Price;
                _selectedFuelMaxVolume = fuelStorage.Volume;
            }
        }
    }
}
