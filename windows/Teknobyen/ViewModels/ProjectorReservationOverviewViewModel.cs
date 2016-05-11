﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;
using Teknobyen.Services.CredentialsService;
using Teknobyen.Services.FirebaseService;
using Teknobyen.Services.SettingsService;
using Teknobyen.Views;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Teknobyen.ViewModels
{
    class ProjectorReservationOverviewViewModel : ViewModelBase
    {
        FirebaseService _firebaseService;


        private List<ProjectorReservationModel> _reservationsList;
        public List<ProjectorReservationModel> ReservationsList
        {
            get { return _reservationsList; }
            set { Set(ref _reservationsList, value); }
        }


        public async override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            try
            {
                _firebaseService = new FirebaseService();
                var list = await _firebaseService.GetReservations();

                list = (from s in list
                        where s.endTime > DateTime.Now
                        select s).ToList();
                list.Sort();
                ReservationsList = list;
            }
            catch (Exception)
            {
                //Log to hockey app
            }
        }

        public void GotoProjectorReservationPage()
        {
            this.NavigationService.Navigate(typeof(ProjectorReservationPage));
            
        }
    }
}
