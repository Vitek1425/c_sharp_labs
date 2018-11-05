using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContactList
{
    public class Contact : INotifyPropertyChanged
    {
        private string _name;
        private string _workPhone;
        private string _homePhone;
        private string _email;
        private long _birthDate;
        private string _comment;

        public int Id { get; set; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == null || !_name.Equals(value))
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string WorkPhone
        {
            get
            {
                return _workPhone;
            }
            set
            {
                if (_workPhone == null || !_workPhone.Equals(value))
                {
                    _workPhone = value;
                    OnPropertyChanged("WorkPhone");
                }
            }
        }
        public string HomePhone
        {
            get
            {
                return _homePhone;
            }
            set
            {
                if (_homePhone == null || !_homePhone.Equals(value))
                {
                    _homePhone = value;
                    OnPropertyChanged("HomePhone");
                }
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email == null || !_email.Equals(value))
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        public long BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                if (_birthDate != value)
                {
                    _birthDate = value;
                    OnPropertyChanged("BirthDate");
                }
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                if (_comment == null || !_comment.Equals(value))
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        public void CopyFromOther(Contact contact)
        {
            if (contact == null)
                return;

            Name = contact._name;
            WorkPhone = contact._workPhone;
            HomePhone = contact._homePhone;
            Email = contact._email;
            BirthDate = contact._birthDate;
            Comment = contact._comment;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
