using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Messages;
using Teknobyen.Models;
using Teknobyen.Services.CredentialsService;
using Teknobyen.Services.FirebaseService;
using Teknobyen.Services.ProjectorReservationService;
using Teknobyen.Services.SettingsService;
using Teknobyen.Views;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Teknobyen.ViewModels
{
    class ProjectorReservationOverviewViewModel : ViewModelBase
    {
        IProjectorReservationService _projectorService;


        private List<ProjectorReservationModel> _reservationsList;
        public List<ProjectorReservationModel> ReservationsList
        {
            get { return _reservationsList; }
            set { Set(ref _reservationsList, value); }
        }


        public ProjectorReservationOverviewViewModel()
        {
            _projectorService = ProjectorReservationService.Instance;
        }

        public async override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            App.EventAggregator.GetEvent<ProjectorReservationsUpdated>().Subscribe(UpdateReservationsList);
            ReservationsList = _projectorService.GetReservations(true).Where(e => e.endTime > DateTime.Now).ToList();
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> pageState, bool suspending)
        {
            App.EventAggregator.GetEvent<ProjectorReservationsUpdated>().Unsubscribe(UpdateReservationsList);
            return base.OnNavigatedFromAsync(pageState, suspending);
        }

        private void UpdateReservationsList(string obj)
        {
            ReservationsList = _projectorService.GetReservations(true).Where(e => e.endTime > DateTime.Now).ToList();
        }

        public void GotoProjectorReservationPage()
        {
            this.NavigationService.Navigate(typeof(ProjectorReservationPage));
            
        }
    }
}
