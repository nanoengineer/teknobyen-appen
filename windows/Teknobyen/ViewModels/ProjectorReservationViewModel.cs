using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;
using Teknobyen.Services.CredentialsService;
using Teknobyen.Services.FirebaseService;
using Teknobyen.Services.SettingsService;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Teknobyen.ViewModels
{
    class ProjectorReservationViewModel : ViewModelBase
    {
        IFirebaseService _firebaseService;
        ISettingsService _settingsService;

        #region Bindable properties
        private DateTimeOffset _selectedDate;
        public DateTimeOffset SelectedDate
        {
            get { return _selectedDate; }
            set { Set(ref _selectedDate, value); }
        }

        private TimeSpan _selectedStartTime = DateTime.Now.TimeOfDay;
        public TimeSpan SelectedStartTime
        {
            get { return _selectedStartTime; }
            set { Set(ref _selectedStartTime, value); }
        }

        
        private ObservableCollection<DurationModel> _durations;
        public ObservableCollection<DurationModel> Durations
        {
            get { return _durations; }
            set { Set(ref _durations, value); }
        }

        private object _selectedDuration;
        public object SelectedDuration
        {
            get { return _selectedDuration; }
            set { Set(ref _selectedDuration, value); }
        }


        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set { Set(ref _comment, value); }
        }
        #endregion

        public ProjectorReservationViewModel()
        {
            _firebaseService = FirebaseService.Instance;
            _settingsService = SettingsService.Instance;
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            SelectedDate = DateTimeOffset.Now;
            SetUpDurations();
            return base.OnNavigatedToAsync(parameter, mode, state);
            
        }

        private void SetUpDurations()
        {
            var t = new ObservableCollection<DurationModel>();
            for (int i = 1; i <= 16; i++)
            {
                t.Add(new DurationModel(TimeSpan.FromMinutes(i * 15)));
            }
            Durations = t;
        }

        public async void SaveReservation()
        {
            var p = CredentialsService.Instance.GetUser();
            var m = new ProjectorReservationModel();
            m.userId = p.UserName;
            m.comment = Comment;
            m.name = _settingsService.Name;
            m.roomNumber = _settingsService.RoomNumber;
            m.date = SelectedDate.DateTime;
            m.startTime = new DateTime(m.date.Year, m.date.Month, m.date.Day, SelectedStartTime.Hours, SelectedStartTime.Minutes, 0);

            if (SelectedDuration == null) return;
            m.endTime = m.startTime.Add((SelectedDuration as DurationModel).Duration);

            await _firebaseService.SaveReservation(m);
            NavigationService.GoBack();
        }


    }
}
