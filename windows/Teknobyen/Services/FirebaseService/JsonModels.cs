using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknobyen.Services.FirebaseService
{
    class ReservationRootModel
    {
        List<ReservationJsonModel> reservations { get; set; }
    }

    public class ReservationJsonModel
    {
        public string comment { get; set; }
        public string date { get; set; }
        public string roomNumber { get; set; }
        public string startHour { get; set; }
        public string stopHour { get; set; }
    }

}
