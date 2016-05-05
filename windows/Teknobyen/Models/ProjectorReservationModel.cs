using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknobyen.Models
{
    class ProjectorReservationModel
    {
        public string comment { get; set; }
        public DateTime date { get; set; }
        public int id { get; set; }
        public int roomNumber { get; set; }
        public DateTime startHour { get; set; }
        public DateTime stopHour { get; set; }
    }


}
