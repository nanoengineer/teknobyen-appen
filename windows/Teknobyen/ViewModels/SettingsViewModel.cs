using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Services.CredentialsService;
using Teknobyen.Services.SettingsService;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Teknobyen.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        ISettingsService _settingsService;
        ICredentialsService _credentialsService;

        public SettingsViewModel()
        {
            _settingsService = SettingsService.Instance;
            _credentialsService = CredentialsService.Instance;
        }


        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            //TODO - auto load settings
            var roomNumber = _settingsService.RoomNumber;
            if (roomNumber != 0) RoomNumber = roomNumber;

            var credentials = _credentialsService.GetUser();
            if (credentials != null)
            {
                Username = credentials.UserName;
                Password = credentials.Password;
            }

            return base.OnNavigatedToAsync(parameter, mode, state);
        }


        private int _roomNumber;
        public int RoomNumber
        {
            get { return _roomNumber; }
            set { Set( ref _roomNumber, value); }
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
            set { Set( ref _password, value); }
        }

        public void SaveSettings()
        {
            _settingsService.RoomNumber = RoomNumber;

            _credentialsService.SaveUser(new Windows.Security.Credentials.PasswordCredential(App.APPID, Username, Password));
        }

    }
}
