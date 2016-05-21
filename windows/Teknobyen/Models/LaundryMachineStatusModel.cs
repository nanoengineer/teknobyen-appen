using Newtonsoft.Json;
using System;

namespace Teknobyen.Models
{
    public class LaundryMachineStatusModel
    {
        public LaundryMachineStatusModel(int _machineId, int _minutesLeft)
        {
            MachineId = _machineId;
            MinutesLeft = _minutesLeft;
        }

        public LaundryMachineStatusModel(int machineId, int minutesLeft, DateTime reservedTime, MachineStatus status)
        {
            MachineId = machineId;
            MinutesLeft = minutesLeft;
            ReservedTime = reservedTime;
            Status = status;
        }

        public int MachineId { get; set; }
        public int MinutesLeft { get; set; }
        public DateTime ReservedTime { get; set; }
        public MachineStatus Status { get; set; }


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

    public enum MachineStatus
    {
        Available,
        Busy,
        Reserved,
        Unknown
    }
}
