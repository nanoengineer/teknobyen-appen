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
        List<string> ValidateWashList(List<WashDayModel> listToValidate);

        List<WashDayModel> GenerateWashList(
            DateTime startDate, DateTime endDate, 
            RoomModel startAt, 
            List<RoomModel> extraRooms = null, 
            List<RoomModel> roomsToSkip = null);

    }
}
