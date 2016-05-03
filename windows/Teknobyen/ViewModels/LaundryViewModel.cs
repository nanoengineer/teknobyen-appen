using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private Uri _washingUrl;
        public Uri WashingUrl
        {
            get { return _washingUrl; }
            set { _washingUrl = value; }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { Set(ref _userName,value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { Set(ref _password, value); }
        }

        private string _htmlContent;
        public string HtmlContent
        {
            get { return _htmlContent; }
            set { Set(ref _htmlContent,  value); }
        }


        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            _laundryService = new LaundryService();
            _credentialsService = new CredentialsService();
            GetLaundrySiteHtml();
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public async void GetLaundrySiteHtml()
        {
            string username = "";
            string password = "";

            var t = _credentialsService.GetUser();
            if (t == null)
            {
                //Ask for password
                username = "AAQA AEAA AAAA C4AW GAB";
                password = "9137f9";
            }
            else
            {
                username = t.UserName;
                password = t.Password;
            }

            var mList = await _laundryService.GetMachineStatusList(username, password);
        }

        
    }
}
