using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;

namespace Teknobyen.Services.WashListService
{
    public class WashListWrapper
    {
        public WashListWrapper(List<WashDayModel> washlist)
        {
            WashList = washlist;
        }
        public List<WashDayModel> WashList { get; set; }

        public bool IsWashdayInList(WashDayModel washday)
        {
            try
            {
                var match = (from m in WashList
                            where m.FBID == washday.FBID && m.Date.Date == washday.Date.Date && m.Assignment == washday.Assignment && m.RoomNumber == washday.RoomNumber
                            select m);
                if (match.Count() > 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public bool IsDateAndAssignmentInList(WashDayModel washday)
        {
            var dateAndAssignment = from w in WashList
                                    where w.Date.Date == washday.Date.Date && w.Assignment == washday.Assignment
                                    select w;
            if (dateAndAssignment.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 

    }
}
