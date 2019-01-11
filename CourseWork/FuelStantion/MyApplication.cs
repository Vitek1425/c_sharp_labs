using System;

namespace FuelStantion
{
    public sealed class MyApplication
    {
        private MyApplication()
        {
        }
        private static readonly Lazy<MyApplication> lazy = new Lazy<MyApplication>(() => new MyApplication());
        public static MyApplication Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        Authorization _authorization = new Authorization();
        public Authorization GetAuthorization()
        {
            return _authorization;
        }

        public void OpenAuthorizationDialog()
        {
            AuthorizationDialog authorizationDialog = new AuthorizationDialog();

            if (authorizationDialog.ShowDialog() == true)
            {
                var authorizatedUser = authorizationDialog.User;
                if (authorizatedUser == null)
                    return;

                _authorization.UserName = authorizatedUser.Login;
                _authorization.UserType = authorizatedUser.Type;
            }
        }

        FuelStorageViewModel _fuelStorageViewModel = new FuelStorageViewModel();
        public FuelStorageViewModel GetFuelStorageViewModel()
        {
            return _fuelStorageViewModel;
        }
    }
}
