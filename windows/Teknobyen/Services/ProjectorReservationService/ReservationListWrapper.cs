using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;

namespace Teknobyen.Services.ProjectorReservationService
{
    class ReservationListWrapper
    {

        public ReservationListWrapper(List<ProjectorReservationModel> reservationList)
        {
            ReservationList = reservationList;
        }

        public List<ProjectorReservationModel> ReservationList { get; set; }

        public bool IsReservationChanged(ProjectorReservationModel reservation)
        {
            var matches = from m in ReservationList
                          where m.reservationId == reservation.reservationId && m.userId == reservation.userId 
                          select m;
            if (matches.Count() > 0)
            {
                var firstMatch = matches.First();
                if (firstMatch.comment != reservation.comment ||
                    firstMatch.date != reservation.date ||
                    firstMatch.startTime != reservation.startTime ||
                    firstMatch.userId != reservation.userId ||
                    firstMatch.roomNumber != reservation.roomNumber)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool IsReservationInList(ProjectorReservationModel reservation)
        {
            var matches = from m in ReservationList
                          where (m.comment == reservation.comment &&
                                m.date == reservation.date &&
                                m.startTime == reservation.startTime &&
                                m.userId == reservation.userId &&
                                m.roomNumber == reservation.roomNumber &&
                                m.endTime == reservation.endTime)
                          select m;
            if (matches.Count() > 0)
            {
                return true;
            }
            return false;
        }                 
    }                     
}                         
                          