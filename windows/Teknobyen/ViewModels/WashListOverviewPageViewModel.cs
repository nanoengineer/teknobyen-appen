using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Common;
using Teknobyen.Models;
using Teknobyen.Services.StorageService;
using Teknobyen.Services.FirebaseService;
using Teknobyen.Services.SettingsService;
using Teknobyen.Services.WashListService;
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
        IWashListService _washListService;

        public WashListOverviewPageViewModel()
        {
            _firebaseService = FirebaseService.Instance;
            _settingsService = SettingsService.Instance;
            _washListService = WashListService.Instance;
        }

        #region Bindable proterties
        private WashDayModel _nextWashDay;
        public WashDayModel NextWashDay
        {
            get { return _nextWashDay; }
            set { Set(ref _nextWashDay, value); }
        }

        public string TimeToNextWashDayText {
            get
            {
                if (NextWashDay == null)
                {
                    return "";
                }
                var daysLeft = (NextWashDay.Date.Subtract(DateTime.Today));
                if (daysLeft.Days > 14)
                {
                    return $"{daysLeft.Days / 7} uker og {daysLeft.Days % 7} dager";
                }
                else
                {
                    return $"{daysLeft.Days} dager";
                }

            }
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
            try
            {
                //WashList = await _firebaseService.GetWashList();
                using (var db = new WashlistContext())
                {
                    WashList = db.Washdays.OrderBy(e => e.Date).ThenBy(e => e.Assignment).ToList();
                }

               
                var roomnumber = _settingsService.RoomNumber;
                if (roomnumber != 0)
                {
                    NextWashDay = (from w in WashList
                                   where w.Date.Date >= DateTime.Today
                                   select w).OrderBy(e => e.Date).ThenBy(e => e.Assignment).ToList().Find(e => e.RoomNumber == roomnumber);
                }

                
                //await _washListService.BackupWashListToFile(WashList);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                System.Diagnostics.Debug.WriteLine(e.InnerException);
            }
         

            //var t = _washListService.GenerateWashList(new DateTime(2016, 5, 23), new DateTime(2016, 8, 5), RoomManager.GetRoomModel(303));
            //var g = _washListService.ValidateWashList(t);
        }

        public void GotoAdminPage()
        {
            this.NavigationService.Navigate(typeof(WashListAdminPage));
        }
    }
}
