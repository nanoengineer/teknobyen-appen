using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;

namespace Teknobyen.Services.WashListService
{
    public class WashListService : IWashListService
    {
        public static WashListService Instance { get; }
        static WashListService()
        {
            Instance = Instance ?? new WashListService();
        }

        //TODO
        public List<WashDayModel> GenerateWashList(DateTime startDate, DateTime endDate, RoomModel startAt, List<RoomModel> extraRooms = null, List<RoomModel> roomsToSkip = null)
        {
            throw new NotImplementedException();
        }



        public List<WashDayModel> ParseTextToWashList(string washListString)
        {
            var ParsedWashList = new List<WashDayModel>();

            List<string> washDayTextLines = washListString.Split('\n').ToList();

            foreach (var washDayLine in washDayTextLines)
            {
                try
                {
                    List<string> washDayElements = washDayLine.Split('\t').ToList();

                    WashDayModel washDay = new WashDayModel();
                    washDay.Date = DateTime.Parse(washDayElements[0]);
                    washDay.Assignment = int.Parse(washDayElements[1]);
                    washDay.RoomNumber = int.Parse(washDayElements[2]);

                    ParsedWashList.Add(washDay);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine($"Parsing line: \"{washDayLine}\" failed");
                }
                

            }

            return ParsedWashList;
        }

        public List<string> ValidateWashList(List<WashDayModel> listToValidate)
        {
            throw new NotImplementedException();
        }
    }
}
