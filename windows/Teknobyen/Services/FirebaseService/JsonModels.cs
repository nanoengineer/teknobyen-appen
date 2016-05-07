using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;

namespace Teknobyen.Services.FirebaseService
{
    class ReservationRootModel
    {
        Dictionary<string, ReservationJsonModel> reservations { get; set; }
    }

    public class ReservationJsonModel
    {
        public string comment { get; set; }
        public string date { get; set; }
        public string roomNumber { get; set; }
        public string startHour { get; set; }
        public string stopHour { get; set; }
    }

    public class WashDayJsonModel
    {
        public WashDayJsonModel() { }

        public WashDayJsonModel(WashDayModel model)
        {
            this.Date = model.Date.ToString("dd.MM.yyyy");
            this.Assignment = model.Assignment;
            this.RoomNumber = model.RoomNumber;
        }

        public string Date { get; set; }
        public int Assignment { get; set; }
        public int RoomNumber { get; set; }
    }

}
