using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknobyen.Models
{
    public class LaundryMachineStatusModel
    {
        public int MachineId { get; set; }
        public bool Available { get; set; }
        public int MinutesLeft { get; set; }
    }
}
