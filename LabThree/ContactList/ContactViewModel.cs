using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ContactList
{
    class ContactViewModel : INotifyPropertyChanged
    {
        ContactsDB _dbConnection;
        RelayCommand _addCommand;
        RelayCommand _editCommand;
        RelayCommand _deleteCommand;
        RelayCommand _setDefaultFilterCommand;
        RelayCommand _setBirthDateFilterCommand;
        IEnumerable<Contact> _contacts;

        public IEnumerable<Contact> Contacts
        {
            get { return _contacts; }
            set
            {
                _contacts = value;
                OnPropertyChanged("Contacts");
            }
        }

        public ContactViewModel()
        {
            _dbConnection = new ContactsDB();
            _dbConnection.Contacts.Load();
            Contacts = _dbConnection.Contacts.Local.ToBindingList();
        }

        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand((o) =>
                  {
                      EditContactDialog addContactDialog = new EditContactDialog();
                      if (addContactDialog.ShowDialog() == true)
                      {
                          Contact contact = addContactDialog.GetContact();
                          if (contact != null)
                          {
                              _dbConnection.Contacts.Add(contact);
                              _dbConnection.SaveChanges();
                          }
                      }
                  }));
            }
        }
        public RelayCommand EditCommand
        {
            get
            {
                return _editCommand ??
                  (_editCommand = new RelayCommand((selectedItem) =>
                  {
                      Contact selectedContact = selectedItem as Contact;
                      if (selectedContact == null)
                          return;

                      EditContactDialog editContactDialog = new EditContactDialog();
                      editContactDialog.SetContact(selectedContact);

                      if (editContactDialog.ShowDialog() == true)
                      {
                          Contact editedContact = _dbConnection.Contacts.Find(selectedContact.Id);
                          Contact newContact = editContactDialog.GetContact();
                          if (editedContact != null && newContact != null)
                          {
                              editedContact.CopyFromOther(newContact);
                              _dbConnection.Entry(editedContact).State = EntityState.Modified;
                              _dbConnection.SaveChanges();
                          }
                      }
                  }));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                  (_deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      Contact contact = selectedItem as Contact;
                      if (contact == null)
                          return;

                      _dbConnection.Contacts.Remove(contact);
                      _dbConnection.SaveChanges();
                  }));
            }
        }

        public RelayCommand SetDefaultFilterCommand
        {
            get
            {
                if (_setDefaultFilterCommand == null)
                {
                   _setDefaultFilterCommand = new RelayCommand((o) =>
                   {
                        Contacts = _dbConnection.Contacts.Local.ToBindingList();
                   });
                }
                return _setDefaultFilterCommand;
            }
        }

        public RelayCommand SetBirthDateFilterCommand
        {
            get
            {
                if (_setBirthDateFilterCommand == null)
                {
                    _setBirthDateFilterCommand = new RelayCommand((o) =>
                    {
                        DateRangeDialog dateRangeDialog = new DateRangeDialog();
                        if (dateRangeDialog.ShowDialog() == true)
                        {
                            long beginUnixDate = dateRangeDialog.BeginUnixDate();
                            long endUnixDate = dateRangeDialog.EndUnixDate();

                            Contacts = _dbConnection.Contacts.Local.Where(e =>
                                e.BirthDate > beginUnixDate &&
                                e.BirthDate < endUnixDate
                            );
                        }
                    });
                }
                return _setBirthDateFilterCommand;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
