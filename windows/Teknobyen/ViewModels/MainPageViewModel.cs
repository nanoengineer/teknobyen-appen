using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using Teknobyen.Views;
using Teknobyen.Services.CredentialsService;
using Windows.Security.Credentials;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using Windows.Web.Http.Filters;
using Teknobyen.Services.FirebaseService;
using Teknobyen.Models;

namespace Teknobyen.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        ICredentialsService _credentialsService;

        public MainPageViewModel()
        {
            _credentialsService = new CredentialsService();
        }

        public void GotoProjectorReservation()
        {
            this.NavigationService.Navigate(typeof(ProjectorReservationsOverviewPage));
        }

        public void GotoLaundryMenu()
        {
            this.NavigationService.Navigate(typeof(LaundryView));
        }

        public void GotoWashlistPage()
        {
            this.NavigationService.Navigate(typeof(WashListOverviewPage));
        }

        public void GotoSettingsPage()
        {
            this.NavigationService.Navigate(typeof(SettingsPage));
        }
    }
}

