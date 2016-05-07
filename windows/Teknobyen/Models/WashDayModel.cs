using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknobyen.Models
{
    public class WashDayModel
    {
        [JsonIgnore]
        public string FBID { get; set; }
        public DateTime Date { get; set; }
        public int Assignment { get; set; }
        public int RoomNumber { get; set; }
    }
}
