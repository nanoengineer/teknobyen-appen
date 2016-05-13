using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Services.FirebaseService;

namespace Teknobyen.Models
{
    public class ProjectorReservationModel : IComparable
    {
        public ProjectorReservationModel() { }
        public ProjectorReservationModel(string id, ReservationJsonModel jsonModel)
        {
            this.reservationId = id;
            this.userId = jsonModel.userId;
            this.comment = jsonModel.comment;
            this.name = jsonModel.name;
            this.roomNumber = int.Parse(jsonModel.roomNumber);
            this.date = DateTime.ParseExact(jsonModel.date, App.DATEFORMAT, CultureInfo.InvariantCulture);
            this.startTime = DateTime.ParseExact(jsonModel.startTime, App.TIMEFORMAT, CultureInfo.InvariantCulture);
            this.startTime = new DateTime(date.Year, date.Month, date.Day, startTime.Hour, startTime.Minute, 0);
            var cultureI = new CultureInfo("en-US");
            this.endTime = startTime.AddMinutes(double.Parse(jsonModel.duration, cultureI.NumberFormat) * 60);
        }

        public string reservationId { get; set; }
        public string userId { get; set; }
        public string comment { get; set; }
        public string name { get; set; }
        public int roomNumber { get; set; }
        public DateTime date { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        public int CompareTo(object obj)
        {
            var compare = obj as ProjectorReservationModel;
            if (this.startTime < compare.startTime)
            {
                return -1;
            }
            else if (this.startTime == compare.startTime)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }


}
