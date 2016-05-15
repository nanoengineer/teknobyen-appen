using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teknobyen.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Teknobyen.Services.PrintService
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WashListPrintPage : Page
    {
        public WashListPrintPage(WashWeekModel washweek)
        {
            this.InitializeComponent();

            populateProperties(washweek);
        }

        public string HeaderText { get; set; }

        #region Roomnumber properties
        public string mon1 { get; set; }
        public string mon2 { get; set; }
        public string tue1 { get; set; }
        public string tue2 { get; set; }
        public string wed1 { get; set; }
        public string wed2 { get; set; }
        public string thu1 { get; set; }
        public string thu2 { get; set; }
        public string fri1 { get; set; }
        public string fri2 { get; set; }
        public string sat1 { get; set; }
        public string sat2 { get; set; }
        public string sun1 { get; set; }
        public string sun2 { get; set; }
        #endregion


        private void populateProperties(WashWeekModel washlist)
        {
            //Heade
            var noCulture = new CultureInfo("nb-NO");
            HeaderText = $"UKE {washlist.WeekNumber} {washlist.StartDate.ToString("m", noCulture)} - {washlist.EndDate.ToString("m", noCulture)}";

            #region Set rooms for different days
            mon1 = getRoomNumber(washlist.WashDays, DayOfWeek.Monday, 1);
            mon2 = getRoomNumber(washlist.WashDays, DayOfWeek.Monday, 2);
            tue1 = getRoomNumber(washlist.WashDays, DayOfWeek.Tuesday, 1);
            tue2 = getRoomNumber(washlist.WashDays, DayOfWeek.Tuesday, 2);
            wed1 = getRoomNumber(washlist.WashDays, DayOfWeek.Wednesday, 1);
            wed2 = getRoomNumber(washlist.WashDays, DayOfWeek.Wednesday, 2);
            thu1 = getRoomNumber(washlist.WashDays, DayOfWeek.Thursday, 1);
            thu2 = getRoomNumber(washlist.WashDays, DayOfWeek.Thursday, 2);
            fri1 = getRoomNumber(washlist.WashDays, DayOfWeek.Friday, 1);
            fri2 = getRoomNumber(washlist.WashDays, DayOfWeek.Friday, 2);
            sat1 = getRoomNumber(washlist.WashDays, DayOfWeek.Saturday, 1);
            sat2 = getRoomNumber(washlist.WashDays, DayOfWeek.Saturday, 2);
            sun1 = getRoomNumber(washlist.WashDays, DayOfWeek.Sunday, 1);
            sun2 = getRoomNumber(washlist.WashDays, DayOfWeek.Sunday, 2);
            #endregion 
        }

        private string getRoomNumber(List<WashDayModel> washlist, DayOfWeek weekday, int assignment)
        {
            try
            {
                var roomnumber = (from w in washlist
                                  where w.Date.DayOfWeek == weekday && w.Assignment == assignment
                                  select w).First().RoomNumber;

                return roomnumber.ToString();

            }
            catch (Exception)
            {
                return "";
            }
        }


        
    }
}
