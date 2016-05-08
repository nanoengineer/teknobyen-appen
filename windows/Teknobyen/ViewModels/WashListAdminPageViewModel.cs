using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;
using Teknobyen.Services.FirebaseService;
using Teknobyen.Services.WashListService;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Teknobyen.ViewModels
{
    class WashListAdminPageViewModel : ViewModelBase
    {
        IFirebaseService _firebaseService;
        IWashListService _washListService;

        public WashListAdminPageViewModel()
        {
            _firebaseService = FirebaseService.Instance;
            _washListService = WashListService.Instance;
        }

        #region Bindable properties
        /*-------------------------------------------
         * Current washlist properties
         ------------------------------------------*/
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


        /*-------------------------------------------
         * ParseWashList section
         ------------------------------------------*/
        private string _washListAsText;
        public string WashListAsText
        {
            get { return _washListAsText; }
            set { Set(ref _washListAsText, value); }
        }

        private List<WashDayModel> _parsedWashDayList;
        public List<WashDayModel> ParsedWashDayList
        {
            get { return _parsedWashDayList; }
            set { Set(ref _parsedWashDayList, value); }
        }

        #endregion


        public async override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            WashDayList = await _firebaseService.GetWashList();
        }

        public void ParseWashListText()
        {
            try
            {
                var toParse = WashListAsText;
                ParsedWashDayList = _washListService.ParseTextToWashList(toParse);
                
            }
            catch (Exception)
            {

            }
            
        }

        public void SaveParsedList()
        {
            try
            {
                if (ParsedWashDayList.Count == 0)
                {
                    return;
                }
                foreach (var item in ParsedWashDayList)
                {
                    _firebaseService.SaveWashDayEntry(item);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Save failed because: {e.InnerException}");
            }
        }

    }
}
