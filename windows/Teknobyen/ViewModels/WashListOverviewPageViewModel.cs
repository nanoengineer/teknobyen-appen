using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;
using Teknobyen.Services.FirebaseService;
using Teknobyen.Services.SettingsService;
using Teknobyen.Views;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Teknobyen.ViewModels
{
    class WashListOverviewPageViewModel : ViewModelBase
    {
        //IWashListService _washlistService;
        IFirebaseService _firebaseService;
        ISettingsService _settingsService;


        public WashListOverviewPageViewModel()
        {
            _firebaseService = FirebaseService.Instance;
            _settingsService = SettingsService.Instance;
        }

        #region Bindable proterties
        private WashDayModel _nextWashDay;
        public WashDayModel NextWashDay
        {
            get { return _nextWashDay; }
            set { Set(ref _nextWashDay, value); }
        }

        private List<WashDayModel> _washList;
        public List<WashDayModel> WashList
        {
            get { return _washList; }
            set { Set( ref _washList, value); }
        }

        private string _washAssignmentText;
        public string WashAssignmentText
        {
            get { return _washAssignmentText; }
            set { Set( ref _washAssignmentText, value); }
        }

        #endregion



        public async override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            
            WashList = await _firebaseService.GetWashList();
            var roomnumber = _settingsService.RoomNumber;
            if (roomnumber != 0)
            {
                NextWashDay = (from w in WashList
                               where w.Date.Date >= DateTime.Today
                               select w).OrderBy(e => e.Date).ThenBy(e => e.Assignment).ToList().Find(e => e.RoomNumber == 503);
            }
        }

        public void GotoAdminPage()
        {
            this.NavigationService.Navigate(typeof(WashListAdminPage));
        }
    }
}
