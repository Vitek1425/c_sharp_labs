using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FuelStantion
{
    public class User : INotifyPropertyChanged
    {
        public int id { get; set; }

        string _login;
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged("Login");
                }
            }
        }

        EUserType _type;
        public EUserType Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        string _passwordHash;
        public string PasswordHash
        {
            get
            {
                return _passwordHash;
            }
            set
            {
                if (_passwordHash != value)
                {
                    _passwordHash = value;
                    OnPropertyChanged("PasswordHash");
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
