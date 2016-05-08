using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Common;
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

        public IList<string> ValidateWashList(List<WashDayModel> listToValidate)
        {
            //Must be sorted by date and then assignment
            listToValidate =  listToValidate.OrderBy(e => e.Date).ThenBy(e => e.Assignment).ToList();


            IList<string> errorList = new List<string>();
            //First ensure there are even entries in the list
            var entriesCount = listToValidate.Count;
            if (entriesCount % 2 != 0)
            {
                //Add error line
                errorList.Add("Incorrect number of entries. Should be even numbered.");
            }

            for (int i = 0; i < (entriesCount/2d); i++)
            {
                //Ensure two entries for each date
                if(listToValidate[2*i].Date.Date != listToValidate[2 * i + 1].Date.Date)
                {
                    errorList.Add($"Date entries not correctly aligned");
                }
                if (listToValidate[2*i].Assignment != 1 || listToValidate[2*i+1].Assignment != 2)
                {
                    errorList.Add("Assignments not correctly numbered eg. not 1 and 2");
                }
                if (RoomManager.IsDoubleRoom(listToValidate[2*i].RoomNumber) || RoomManager.IsDoubleRoom(listToValidate[2 * i + 1].RoomNumber))
                {
                    if (listToValidate[2*i].RoomNumber != listToValidate[2*i+1].RoomNumber)
                    {
                        errorList.Add($"Doubleroom not having both assignments on date {listToValidate[2*i].Date.ToString()}");
                    }
                }

            }

            return errorList;
        }
    }
}
