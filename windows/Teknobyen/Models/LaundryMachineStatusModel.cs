using Newtonsoft.Json;

namespace Teknobyen.Models
{
    public class LaundryMachineStatusModel
    {
        public LaundryMachineStatusModel(int _machineId, int _minutesLeft)
        {
            MachineId = _machineId;
            MinutesLeft = _minutesLeft;
        }

        public int MachineId { get; set; }
        public int MinutesLeft { get; set; }

        [JsonIgnore]
        public bool Available {
            get
            {
                if (MinutesLeft == -1)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
