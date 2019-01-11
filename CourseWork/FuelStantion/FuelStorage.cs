using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FuelStantion
{
    public class FuelStorage : INotifyPropertyChanged
    {
        [Key]
        [ForeignKey("FuelType")]
        public int fuel_type_id { get; set; }

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
