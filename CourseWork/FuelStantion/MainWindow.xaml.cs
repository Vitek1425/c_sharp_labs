using System;
using System.Windows;
using System.ComponentModel;

namespace FuelStantion
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MyApplication.Instance.GetAuthorization().PropertyChanged += OnAuthorizationChnaged;
            this.DataContext = MyApplication.Instance.GetFuelStorageViewModel();
            SetUserType(EUserType.NotConnected);
        }

        private void OnAuthorizationChnaged(object sender, PropertyChangedEventArgs e)
        {
            var authorization = MyApplication.Instance.GetAuthorization();
            StatusBar.Text = String.Format("You are logged like {0} ({1})", authorization.UserName, authorization.UserType.ToString());
            SetUserType(authorization.UserType);
        }

        private void SetUserType(EUserType userType)
        {
            switch (userType)
            {
                case EUserType.NotConnected:
                    {
                        AddUserMenu.Visibility = Visibility.Collapsed;
                        OrderMenu.Visibility = Visibility.Collapsed;
                        PlotsMenu.Visibility = Visibility.Collapsed;
                        UsersGridItem.Visibility = Visibility.Collapsed;
                        EditRefillingMenuItem.Visibility = Visibility.Collapsed;
                        RefillingTabItem.Visibility = Visibility.Collapsed;
                        StorageTabItem.Visibility = Visibility.Collapsed;
                        EditStorageMenu.Visibility = Visibility.Collapsed;
                        break;
                    }
                case EUserType.Seller:
                    {
                        AddUserMenu.Visibility = Visibility.Collapsed;
                        OrderMenu.Visibility = Visibility.Visible;
                        PlotsMenu.Visibility = Visibility.Visible;
                        UsersGridItem.Visibility = Visibility.Collapsed;
                        EditRefillingMenuItem.Visibility = Visibility.Collapsed;
                        RefillingTabItem.Visibility = Visibility.Visible;
                        StorageTabItem.Visibility = Visibility.Visible;
                        EditStorageMenu.Visibility = Visibility.Collapsed;
                        break;
                    }
                case EUserType.Admin:
                    {
                        AddUserMenu.Visibility = Visibility.Visible;
                        OrderMenu.Visibility = Visibility.Visible;
                        PlotsMenu.Visibility = Visibility.Visible;
                        UsersGridItem.Visibility = Visibility.Visible;
                        EditRefillingMenuItem.Visibility = Visibility.Visible;
                        RefillingTabItem.Visibility = Visibility.Visible;
                        StorageTabItem.Visibility = Visibility.Visible;
                        EditStorageMenu.Visibility = Visibility.Visible;
                        break;
                    }
            }
        }

        private void AuthorizationMenu_Click(object sender, RoutedEventArgs e)
        {
            MyApplication.Instance.OpenAuthorizationDialog();
        }
    }
}
