using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;
using Teknobyen.Services.FirebaseService;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Teknobyen.ViewModels
{
    class WashListAdminPageViewModel : ViewModelBase
    {
        IFirebaseService _firebaseService;

        public WashListAdminPageViewModel()
        {
            _firebaseService = FirebaseService.Instance;
        }

        #region Bindable properties
        private List<WashDayModel> _washDayList;
        public List<WashDayModel> WashDayList
        {
            get { return _washDayList; }
            set { Set(ref _washDayList, value); }
        }

        private WashDayModel _selectedWashDay;
        public WashDayModel SelectedWashDay
        {
            get { return _selectedWashDay; }
            set { Set( ref _selectedWashDay, value); }
        }



        #endregion


        public async override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            WashDayList = await _firebaseService.GetWashList();
        }


    }
}
