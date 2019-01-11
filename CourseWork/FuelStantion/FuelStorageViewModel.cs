using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FuelStantion
{
    public class FuelStorageViewModel : INotifyPropertyChanged
    {
        FuelStorageDB _dbConnection;
        public FuelStorageDB DBConnection => _dbConnection;

        IEnumerable<FuelStorage> _fuelStorages;
        public IEnumerable<FuelStorage> FuelStorages
        {
            get { return _fuelStorages; }
            set
            {
                _fuelStorages = value;
                OnPropertyChanged("FuelStorages");
            }
        }

        IEnumerable<FuelRefilling> _fuelRefillings;
        public IEnumerable<FuelRefilling> FuelRefillings
        {
            get { return _fuelRefillings; }
            set
            {
                _fuelRefillings = value;
                OnPropertyChanged("FuelRefillings");
            }
        }

        IEnumerable<User> _users;
        public IEnumerable<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged("Users");
            }
        }

        IEnumerable<FuelType> _fuelTypes;
        public IEnumerable<FuelType> FuelTypes
        {
            get { return _fuelTypes; }
            set
            {
                _fuelTypes = value;
                OnPropertyChanged("FuelTypes");
            }
        }

        RelayCommand _addUserCommand;
        public RelayCommand AddUserCommand
        {
            get
            {
                return _addUserCommand ??
                (_addUserCommand = new RelayCommand((o) =>
                {
                    var dialog = new EditUserDialog();

                    if (dialog.ShowDialog() == true)
                    {
                        User userData = new User();
                        dialog.CopyValuesToUserData(userData);
                        _dbConnection.Users.Add(userData);
                        _dbConnection.SaveChanges();
                    }
                }));
            }
        }

        RelayCommand _showFuelVolumePlotCommand;
        public RelayCommand ShowFuelVolumePlotCommand
        {
            get
            {
                return _showFuelVolumePlotCommand ??
                (_showFuelVolumePlotCommand = new RelayCommand((o) =>
                {
                    Window dialog = new Window
                    {
                        Title = "Fuel Plot",
                        Content = new OilVolumePlot()
                    };

                    dialog.ShowDialog();
                }));
            }
        }

        RelayCommand _editStorageCommand;
        public RelayCommand EditStorageCommand
        {
            get
            {
                return _editStorageCommand ??
                (_editStorageCommand = new RelayCommand((selectedItem) =>
                {
                    FuelStorage selectedStorage = selectedItem as FuelStorage;
                    if (selectedStorage == null)
                        return;

                    var dialog = new EditStorageDialog();
                    dialog.FuelName = selectedStorage.FuelType.Name;
                    dialog.Volume = selectedStorage.Volume;
                    dialog.Price = selectedStorage.Price;

                    if (dialog.ShowDialog() == true)
                    {
                        selectedStorage.Volume = dialog.Volume;
                        selectedStorage.Price = dialog.Price;
                        _dbConnection.Entry(selectedStorage).State = EntityState.Modified;
                        _dbConnection.SaveChanges();
                    }
                }));
            }
        }

        RelayCommand _editUserPasswordCommand;
        public RelayCommand EditUserPasswordCommand
        {
            get
            {
                return _editUserPasswordCommand ??
                (_editUserPasswordCommand = new RelayCommand((selectedItem) =>
                {
                    User selectedUser = selectedItem as User;
                    if (selectedUser == null)
                        return;

                    var dialog = new EditPasswordDialog();

                    if (dialog.ShowDialog() == true)
                    {
                        selectedUser.PasswordHash = Utils.GetHashString(dialog.Password);
                        _dbConnection.Entry(selectedUser).State = EntityState.Modified;
                        _dbConnection.SaveChanges();
                    }
                }));
            }
        }

        RelayCommand _editUserTypeCommand;
        public RelayCommand EditUserTypeCommand
        {
            get
            {
                return _editUserTypeCommand ??
                (_editUserTypeCommand = new RelayCommand((selectedItem) =>
                {
                    User selectedUser = selectedItem as User;
                    if (selectedUser == null)
                        return;

                    var dialog = new EditUserTypeDialog();
                    dialog.Type = selectedUser.Type;

                    if (dialog.ShowDialog() == true)
                    {
                        selectedUser.Type = dialog.Type;;
                        _dbConnection.Entry(selectedUser).State = EntityState.Modified;
                        _dbConnection.SaveChanges();

                        if (selectedUser.Login == MyApplication.Instance.GetAuthorization().UserName)
                            MyApplication.Instance.GetAuthorization().UserType = selectedUser.Type;
                    }
                }));
            }
        }

        RelayCommand _addRefillingCommand;
        public RelayCommand AddRefillingCommand
        {
            get
            {
                return _addRefillingCommand ??
                  (_addRefillingCommand = new RelayCommand((o) =>
                  {
                      var dialog = new AddRefillingDialog();
                      dialog.OperatorName = MyApplication.Instance.GetAuthorization().UserName;
                      if (dialog.ShowDialog() == true)
                      {
                          FuelRefilling fuelRefilling = new FuelRefilling();
                          dialog.CopyValuesToFuelRefilling(fuelRefilling);
                          _dbConnection.FuelRefillings.Add(fuelRefilling);

                          var fuelStorage = _dbConnection.FuelStorages.Find(fuelRefilling.Fuel_type_id);
                          if (fuelStorage != null)
                          {
                              fuelStorage.Volume -= fuelRefilling.Volume;
                              _dbConnection.Entry(fuelStorage).State = EntityState.Modified;
                          }

                          _dbConnection.SaveChanges();
                      }
                  }));
            }
        }

        RelayCommand _editRefillingCommand;
        public RelayCommand EditRefillingCommand
        {
            get
            {
                return _editRefillingCommand ??
                  (_editRefillingCommand = new RelayCommand((selectedItem) =>
                  {
                      FuelRefilling selectedRefilling = selectedItem as FuelRefilling;
                      if (selectedRefilling == null)
                          return;

                      var dialog = new AddRefillingDialog();
                      dialog.IsEditMode = true;
                      dialog.CopyValuesFromFuelRefilling(selectedRefilling);

                      if (dialog.ShowDialog() == true)
                      {
                          dialog.CopyValuesToFuelRefilling(selectedRefilling);
                          _dbConnection.Entry(selectedRefilling).State = EntityState.Modified;
                          _dbConnection.SaveChanges();
                      }
                  }));
            }
        }

        public FuelStorageViewModel()
        {
            _dbConnection = new FuelStorageDB();

            _dbConnection.FuelTypes.Load();
            FuelTypes = _dbConnection.FuelTypes.Local.ToBindingList();

            _dbConnection.Users.Load();
            Users = _dbConnection.Users.Local.ToBindingList();

            _dbConnection.FuelStorages.Load();
            FuelStorages = _dbConnection.FuelStorages.Local.ToBindingList();

            _dbConnection.FuelRefillings.Load();
            FuelRefillings = _dbConnection.FuelRefillings.Local.ToBindingList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
