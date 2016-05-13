using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknobyen.Models
{
    public class WashWeekModel
    {
        public WashWeekModel(DateTime startDate, List<WashDayModel> _washdays)
        {

        }

        public DateTime StartDate { get; set; }


        public int WeekNumber { get; set; }

        //Returns startdate pluss 7. Startdate must be a monday
        public DateTime EndDate {
            get
            {
                return StartDate.AddDays(7);
            }
        }

        public List<WashDayModel> WashDays { get; set; }
    }
}
