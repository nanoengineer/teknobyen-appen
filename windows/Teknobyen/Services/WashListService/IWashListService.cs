using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;

namespace Teknobyen.Services.WashListService
{
    interface IWashListService
    {
        List<WashDayModel> GetWashList(bool syncAfterRetieve);
        void SyncWashList();
        


        List<WashDayModel> GenerateWashList(
            DateTime startDate, DateTime endDate, 
            RoomModel startAt, 
            List<RoomModel> extraRooms = null, 
            List<RoomModel> roomsToSkip = null);

        List<WashWeekModel> GetPrintableWashList(List<WashDayModel> listToPrint);
        Task<bool> BackupWashListToFile(List<WashDayModel> listToBackup);
        List<WashDayModel> GetWashListBetweenDates(DateTime startDate, DateTime endDate, List<WashDayModel> washList);
        List<WashDayModel> ParseTextToWashList(string washListString);
        List<string> ValidateWashList(List<WashDayModel> listToValidate);
    }
}
