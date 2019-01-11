using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelStantion
{
    public class FuelRefilling : INotifyPropertyChanged
    {
        public int Id { get; set; }

        string _operatorName = "";
        public string OperatorName
        {
            get
            {
                return _operatorName;
            }
            set
            {
                if (_operatorName != value)
                {
                    _operatorName = value;
                    OnPropertyChanged("OperatorName");
                }
            }
        }

        private long _date = 0;
        public long Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        int _fuel_type_id = 0;
        [ForeignKey("FuelType")]
        public int Fuel_type_id
        {
            get
            {
                return _fuel_type_id;
            }
            set
            {
                if (_fuel_type_id != value)
                {
                    _fuel_type_id = value;
                    OnPropertyChanged("Fuel_type_id");
                }
            }
        }

        public FuelType FuelType { get; set; }

        private double _price = 0.0;
        public double Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (!Utils.AreEqual(_price, value))
                {
                    _price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        private double _volume = 0.0;
        public double Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                if (!Utils.AreEqual(_volume, value))
                {
                    _volume = value;
                    OnPropertyChanged("Volume");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
