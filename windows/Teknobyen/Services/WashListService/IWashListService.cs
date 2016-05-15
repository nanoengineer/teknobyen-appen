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
        List<WashDayModel> ParseTextToWashList(string washListString);
        IList<string> ValidateWashList(List<WashDayModel> listToValidate);

        List<WashDayModel> GenerateWashList(
            DateTime startDate, DateTime endDate, 
            RoomModel startAt, 
            List<RoomModel> extraRooms = null, 
            List<RoomModel> roomsToSkip = null);

        List<WashWeekModel> GetPrintableWashList(List<WashDayModel> listToPrint);

        Task<bool> BackupWashListToFile(List<WashDayModel> listToBackup);

    }
}
