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
using Windows.UI.Xaml.Media.Animation;

namespace Teknobyen.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        ICredentialsService _credentialsService;

        public MainPageViewModel()
        {
            _credentialsService = new CredentialsService();
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            //App.EventAggregator.GetEvent<Messages.EventTest>().Subscribe(WriteDebug);

            return Task.CompletedTask;
        }

        private void WriteDebug(string obj)
        {
            System.Diagnostics.Debug.WriteLine($"Event recieved at: {DateTime.Now.TimeOfDay}");
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

