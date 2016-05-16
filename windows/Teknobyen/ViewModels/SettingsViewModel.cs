using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Common;
using Teknobyen.Services.CredentialsService;
using Teknobyen.Services.LaundryService;
using Teknobyen.Services.SettingsService;
using Teknobyen.Views;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Teknobyen.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        ISettingsService _settingsService;
        ICredentialsService _credentialsService;
        ILaundryService _laundryService;

        public SettingsViewModel()
        {
            _settingsService = SettingsService.Instance;
            _credentialsService = CredentialsService.Instance;
            _laundryService = LaundryService.Instance;
        }

        #region Bindable properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private string _roomNumber;
        public string RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                string numString = new string(value.Where(c => char.IsDigit(c)).ToArray());
                Set(ref _roomNumber, numString);
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { Set(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { Set(ref _password, value); }
        }

        private string _adminPassword;
        public string AdminPassword
        {
            get { return _adminPassword; }
            set { Set( ref _adminPassword, value); }
        }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { Set( ref _isAdmin, value); }
        }


        #endregion


        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Name = _settingsService.Name;
            IsAdmin = _settingsService.IsAdmin;
            RoomNumber = _settingsService.RoomNumber.ToString();

            var credentials = _credentialsService.GetUser();
            if (credentials != null)
            {
                Username = credentials.UserName;
                Password = credentials.Password;
            }

            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public async void SaveSettings()
        {
            bool allSettingsValid = true;
            if (Name.Length < 2 || !RoomManager.IsValidRoom(int.Parse(RoomNumber)))
            {
                allSettingsValid = false;
            }
            else
            {
                _settingsService.Name = Name;
                _settingsService.RoomNumber = int.Parse(RoomNumber);
            }
            
            if (AdminPassword == "adMin")
            {
                IsAdmin = true;
                _settingsService.IsAdmin = true;
            }

            if (await _laundryService.IsValidCredentials(Username, Password))
            {
                _credentialsService.SaveUser(new Windows.Security.Credentials.PasswordCredential(App.APPID, Username, Password));
            }
            else
            {
                allSettingsValid = false;
            }


            if (allSettingsValid)
            {
                if (this.NavigationService.CanGoBack)
                    NavigationService.GoBack();
                _settingsService.FirstRunCompleted = true;

                NavigationService.Navigate(typeof(MainPage));
            }


            
        }

        public void LogoutOfAdmin()
        {
            _settingsService.IsAdmin = false;
            IsAdmin = false;
        }

    }
}
