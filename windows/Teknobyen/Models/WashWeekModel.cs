using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Common;

namespace Teknobyen.Models
{
    public class WashWeekModel
    {
        public WashWeekModel(int _weekNumber, int _year, List<WashDayModel> _washdays)
        {
            WeekNumber = _weekNumber;
            Year = _year;
            WashDays = _washdays;
        }

        
        public int WeekNumber { get; set; }
        public int Year { get; set; }

        //Gets the first date(monday) from the given week and YearNnumber
        public DateTime StartDate { get
            {
                return Utils.FirstDateOfWeekISO8601(Year, WeekNumber);
            }
        }
        //Returns startdate pluss 6. Startdate must be a monday
        public DateTime EndDate {
            get
            {
                return StartDate.AddDays(6);
            }
        }

        public List<WashDayModel> WashDays { get; set; }
    }
}
