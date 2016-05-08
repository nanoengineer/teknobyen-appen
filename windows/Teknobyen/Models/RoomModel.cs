using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknobyen.Models
{
    public class RoomModel
    {
        public RoomModel() { }
        public RoomModel(int number, bool doubleR)
        {
            RoomNumber = number;
            DoubleRoom = doubleR;
        }

        public int RoomNumber { get; set; }
        public bool DoubleRoom { get; set; }
    }
}
