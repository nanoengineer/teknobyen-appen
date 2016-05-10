using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;

namespace Teknobyen.Common
{
    static class RoomManager
    {
        public static List<RoomModel> AllRooms = new List<RoomModel>
        #region Init all rooms list
        {
            new RoomModel(201, false),
            new RoomModel(202, false),
            new RoomModel(203, false),
            new RoomModel(204, false),
            new RoomModel(205, false),
            new RoomModel(206, false),
            new RoomModel(207, true),
            new RoomModel(208, true),
            new RoomModel(209, true),
            new RoomModel(210, true),
            new RoomModel(211, true),
            new RoomModel(301, false),
            new RoomModel(302, false),
            new RoomModel(303, false),
            new RoomModel(304, false),
            new RoomModel(305, false),
            new RoomModel(306, false),
            new RoomModel(307, false),
            new RoomModel(308, false),
            new RoomModel(309, false),
            new RoomModel(310, false),
            new RoomModel(311, false),
            new RoomModel(312, false),
            new RoomModel(313, false),
            new RoomModel(314, false),
            new RoomModel(315, false),
            new RoomModel(316, false),
            new RoomModel(317, false),
            new RoomModel(318, false),
            new RoomModel(319, false),
            new RoomModel(320, false),
            new RoomModel(321, false),
            new RoomModel(322, false),
            new RoomModel(323, false),
            new RoomModel(324, true),
            new RoomModel(325, true),
            new RoomModel(326, false),
            new RoomModel(327, false),
            new RoomModel(401, false),
            new RoomModel(402, false),
            new RoomModel(403, false),
            new RoomModel(404, false),
            new RoomModel(405, false),
            new RoomModel(406, false),
            new RoomModel(407, false),
            new RoomModel(408, false),
            new RoomModel(409, false),
            new RoomModel(410, false),
            new RoomModel(411, false),
            new RoomModel(412, false),
            new RoomModel(413, false),
            new RoomModel(414, false),
            new RoomModel(415, false),
            new RoomModel(416, false),
            new RoomModel(417, false),
            new RoomModel(418, false),
            new RoomModel(419, false),
            new RoomModel(420, false),
            new RoomModel(421, false),
            new RoomModel(422, false),
            new RoomModel(423, false),
            new RoomModel(424, true),
            new RoomModel(425, true),
            new RoomModel(426, false),
            new RoomModel(427, false),
            new RoomModel(501, false),
            new RoomModel(502, false),
            new RoomModel(503, false),
            new RoomModel(504, false),
            new RoomModel(505, false),
            new RoomModel(506, false),
            new RoomModel(507, false),
            new RoomModel(508, false),
            new RoomModel(509, false),
            new RoomModel(510, false),
            new RoomModel(511, false),
            new RoomModel(512, false),
            new RoomModel(513, false),
            new RoomModel(514, true),
            new RoomModel(515, false),
            new RoomModel(516, false),
            new RoomModel(517, false),
            new RoomModel(518, false),
            new RoomModel(519, false),
            new RoomModel(520, true),
            new RoomModel(521, true),
            new RoomModel(522, false),
            new RoomModel(523, false),
            new RoomModel(601, false),
            new RoomModel(602, false),
            new RoomModel(603, false),
            new RoomModel(604, false),
            new RoomModel(605, false),
            new RoomModel(606, false),
            new RoomModel(607, false),
            new RoomModel(608, false),
            new RoomModel(609, false),
            new RoomModel(610, false),
            new RoomModel(611, false),
            new RoomModel(612, false),
            new RoomModel(613, false),
            new RoomModel(614, false),
            new RoomModel(615, false),
            new RoomModel(616, false)
        };
        #endregion

        public static bool IsDoubleRoom(RoomModel room)
        {
            return (from r in AllRooms
                    where r.RoomNumber == room.RoomNumber
                    select r).First().DoubleRoom;
        }

        public static bool IsValidRoom(int room)
        {
            if ((from r in AllRooms
                where r.RoomNumber == room
                select r).ToList().Count == 1)
            {
                return true;
            }
            return false;
        }
        public static bool IsDoubleRoom(int room)
        {
             var t =  (from r in AllRooms
                       where r.RoomNumber == room
                       select r).ToList();

            if (t.Count < 1) return false;
            else
            {
                return t.First().DoubleRoom;
            }
        }

        public static List<RoomModel> GetContinuousListOfRooms(int beginning, int count, List<RoomModel> exclude = null)
        {
            var roomList = new List<RoomModel>();
            
            var roomNumbersToExclude = new List<int>();
            if (exclude != null)
            {
                foreach (var item in exclude)
                {
                    roomNumbersToExclude.Add(item.RoomNumber);
                }
            }

            while (roomList.Count < count)
            {
                var rooms = (from r in AllRooms
                             where (r.RoomNumber >= beginning)
                             select r).ToList().OrderBy(e => e.RoomNumber).ToList();

                if (exclude != null)
                {
                    rooms = (from r in rooms
                             where (roomNumbersToExclude.Contains(r.RoomNumber) != true)
                             select r).ToList().OrderBy(e => e.RoomNumber).ToList();
                }
                

                if (rooms.Count > (count-roomList.Count))
                {
                    rooms = rooms.GetRange(0, (count - roomList.Count));
                    roomList.AddRange(rooms);
                    return roomList;
                }
                else
                {
                    roomList.AddRange(rooms);

                    //It gets here if it has selected all rooms from beginning to last roomnumber. 
                    //beginning should therefore be set to 201, as it "wraps around"
                    beginning = 201;
                }
            }

            return roomList;
        }

        public static RoomModel GetRoomModel(int roomNumber)
        {
            var room = (from r in AllRooms
                        where r.RoomNumber == roomNumber
                        select r).ToList();
            if (room.Count == 0)
            {
                return null;
            }
            return room.First();
        }
    }
}
