using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FuelStantion
{
    public class Authorization : INotifyPropertyChanged
    {
        EUserType _userType = EUserType.NotConnected;
        public EUserType UserType
        {
            get
            {
                return _userType;
            }
            set
            {
                if (_userType != value)
                {
                    _userType = value;
                    OnPropertyChanged("userType");
                }
            }
        }

        string _userName = "";
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (_userName == null || !_userName.Equals(value))
                {
                    _userName = value;
                    OnPropertyChanged("userName");
                }
            }
        }

        public void Reset()
        {
            UserType = EUserType.NotConnected;
            UserName = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
