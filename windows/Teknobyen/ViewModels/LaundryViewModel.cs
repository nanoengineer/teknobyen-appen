using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;
using Teknobyen.Services.CredentialsService;
using Teknobyen.Services.LaundryService;
using Template10.Mvvm;
using Windows.Security.Credentials;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace Teknobyen.ViewModels
{
    public class LaundryViewModel : ViewModelBase
    {

        ICredentialsService _credentialsService;
        ILaundryService _laundryService;


        private PasswordCredential _credentials;
        public PasswordCredential Credentials
        {
            get { return _credentials; }
            set
            {
                Set(ref _credentials, value);
                if(value != null) _credentialsService.SaveUser(value);
            }
        }

        private ObservableCollection<LaundryMachineStatusModel> _statusCollection = new ObservableCollection<LaundryMachineStatusModel>();
        public ObservableCollection<LaundryMachineStatusModel> StatusCollection {
            get { return _statusCollection; }
            set { Set(ref _statusCollection, value); }
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            _laundryService = new LaundryService();
            _credentialsService = new CredentialsService();
            GetLaundryMachineStatusList();
            //GetLaundryAccountBalance(); FIkk nettvekrsfeil i svaret...
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public async void GetLaundryMachineStatusList()
        {
            var loginCredentials = _credentialsService.GetUser();
            if (loginCredentials == null)
            {
                Credentials = null;
                return;
            }
            else
            {
                Credentials = loginCredentials;
            }

            var mList = await _laundryService.GetMachineStatusList(loginCredentials.UserName, loginCredentials.Password);
            StatusCollection = mList;
        }

        public async void GetLaundryAccountBalance()
        {
            var loginCredentials = _credentialsService.GetUser();
            if (loginCredentials == null)
            {
                Credentials = null;
                return;
            }
            else
            {
                Credentials = loginCredentials;
            }

            var b = await _laundryService.GetAccountBalance(loginCredentials.UserName, loginCredentials.Password);
        }

        
        
    }
}
