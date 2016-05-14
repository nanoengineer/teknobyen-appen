using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Common;
using Teknobyen.Models;
using Teknobyen.Services.FirebaseService;
using Teknobyen.Services.PrintService;
using Teknobyen.Services.WashListService;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace Teknobyen.ViewModels
{
    class WashListAdminPageViewModel : ViewModelBase
    {
        PrintService _printService;
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

        /*-------------------------------------------
         * Generate washlist section
         ------------------------------------------*/

        private DateTimeOffset _startGenerationDate;
        public DateTimeOffset StartGenerationDate
        {
            get { return _startGenerationDate; }
            set { Set(ref _startGenerationDate, value); }
        }

        private DateTimeOffset _endGenerationDate;
        public DateTimeOffset EndGenerationDate
        {
            get { return _endGenerationDate; }
            set { Set(ref _endGenerationDate, value); }
        }

        private string _startRoom;
        public string StartRoom
        {
            get { return _startRoom; }
            set { Set(ref _startRoom, value); }
        }

        private string _redoWashRoomList = "";
        public string RedoWashRoomList
        {
            get { return _redoWashRoomList; }
            set { Set(ref _redoWashRoomList, value); }
        }

        private string _excemptFromWashRoomList = "";
        public string ExcemptFromWashRoomList
        {
            get { return _excemptFromWashRoomList; }
            set { Set(ref _excemptFromWashRoomList, value); }
        }

        private List<WashDayModel> _generatedWashDayList;
        public List<WashDayModel> GeneratedWashDayList
        {
            get { return _generatedWashDayList; }
            set { Set(ref _generatedWashDayList, value); }
        }

        #endregion


        public async override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            try
            {
                WashDayList = (await _firebaseService.GetWashList()).OrderBy(e => e.Date).ThenBy(e => e.Assignment).ToList();

                StartGenerationDate = WashDayList.Last().Date.AddDays(1);
                StartRoom = (WashDayList.Last().RoomNumber +1).ToString();
            }
            catch (Exception)
            {
                StartGenerationDate = DateTimeOffset.Now;
                //Log
            }
            
            EndGenerationDate = DateTimeOffset.Now.AddDays(28);
            //_printService = new PrintService();
            //_printService.RegisterForPrinting();
                
        }

        public async void OnPrintButtonClick()
        {
            //Done in codebehind because you need a canvas from the page
        }

        public void ParseWashListText()
        {
            try
            {
                var toParse = WashListAsText;
                ParsedWashDayList = _washListService.ParseTextToWashList(toParse);
                var errors = _washListService.ValidateWashList(ParsedWashDayList);                
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

        public void GenerateWashList()
        {
            DateTime startDate = StartGenerationDate.DateTime;
            DateTime endDate = EndGenerationDate.DateTime;

            RoomModel startRoom = RoomManager.GetRoomModel(201);
            try
            {
                var s = RoomManager.GetRoomModel(int.Parse(StartRoom));
                if (s != null) startRoom = s;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Couldn't get room");
            }
            

            List<string> redoStrings = RedoWashRoomList.Split().ToList();
            List<string> excemptStrings = ExcemptFromWashRoomList.Split().ToList();

            List<RoomModel> redoRooms = new List<RoomModel>();
            foreach (var roomString in redoStrings)
            {
                try
                {
                    RoomModel room = RoomManager.GetRoomModel(int.Parse(roomString));
                    if (room != null)
                    {
                        redoRooms.Add(room);
                    }
                }
                catch (Exception)
                {
                    //
                }
            }

            List<RoomModel> excemptRooms = new List<RoomModel>();
            foreach (var roomString in excemptStrings)
            {
                try
                {
                    RoomModel room = RoomManager.GetRoomModel(int.Parse(roomString));
                    if (room != null)
                    {
                        excemptRooms.Add(room);
                    }
                }
                catch (Exception)
                {
                    //
                }
            }

            var generatedList = _washListService.GenerateWashList(startDate, endDate, startRoom, redoRooms, excemptRooms);
            GeneratedWashDayList = generatedList;
        }

        public void SaveGeneratedList()
        {
            try
            {
                if (GeneratedWashDayList.Count == 0)
                {
                    return;
                }
                foreach (var item in GeneratedWashDayList)
                {
                    _firebaseService.SaveWashDayEntry(item);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Save failed because: {e.InnerException}");
            }
        }

        public void ClearGeneratedList()
        {
            GeneratedWashDayList = new List<WashDayModel>();
        }
    }
}
