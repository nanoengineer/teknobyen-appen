using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;

namespace Teknobyen.Services.FirebaseService
{
    interface IFirebaseService
    {
        Task<List<ProjectorReservationModel>> GetReservations();
        Task<bool> SaveReservation(ProjectorReservationModel s);
        Task<List<WashDayModel>> GetWashList();
        Task<bool> SaveWashDayEntry(WashDayModel washDay);
        Task<bool> SaveWashDayEntries(List<WashDayModel> washDayList);
        Task<bool> UpdateWashDayEntry(WashDayModel washDay);
        Task<bool> DeleteWashDayEntry(WashDayModel washDay);
        Task<bool> DeleteWashDayEntries(List<WashDayModel> washDaysToDelete);        
        Task<WashDayModel> GetNextWashDay(RoomModel room);


    }
}
