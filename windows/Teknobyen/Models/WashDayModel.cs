using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Services.FirebaseService;

namespace Teknobyen.Models
{
    public class WashDayModel
    {
        public WashDayModel() { }
        public WashDayModel(string id, WashDayJsonModel jsonModel)
        {
            FBID = id;
            Date = DateTime.ParseExact(jsonModel.date, App.DATEFORMAT, CultureInfo.InvariantCulture);
            Assignment = jsonModel.assignment;
            RoomNumber = jsonModel.roomNumber;
        }

        [JsonIgnore]
        public string FBID { get; set; }
        public DateTime Date { get; set; }
        public int Assignment { get; set; }
        public int RoomNumber { get; set; }
    }
}
