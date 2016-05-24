using Newtonsoft.Json;
using System;
using Template10.Mvvm;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Teknobyen.Models
{
    public class LaundryMachineStatusModel : IBindable
    {
        public LaundryMachineStatusModel()
        {
            Status = MachineStatus.Unknown;
        }
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
        public LaundryMachineStatusModel Model { get { return this; } }

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

        public event PropertyChangedEventHandler PropertyChanged;


        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            throw new NotImplementedException();
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
