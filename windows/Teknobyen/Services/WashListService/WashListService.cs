using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Common;
using Teknobyen.Models;
using Windows.Storage;

namespace Teknobyen.Services.WashListService
{
    public class WashListService : IWashListService
    {
        public static WashListService Instance { get; }
        static WashListService()
        {
            Instance = Instance ?? new WashListService();
        }

        
        public List<WashDayModel> GenerateWashList(DateTime startDate, DateTime endDate, RoomModel startAt, List<RoomModel> extraRooms = null, List<RoomModel> roomsToSkip = null)
        {
            int washDaysToCreate = (int)(endDate.Subtract(startDate)).TotalDays;
            var roomList = RoomManager.GetContinuousListOfRooms(startAt.RoomNumber, (2 * washDaysToCreate) + 10);

            var generatedWashList = new List<WashDayModel>();


            int assignment = 1;
            List<RoomModel> doubleRoomsOnHold = new List<RoomModel>();
            var currentDate = startDate;
            while (currentDate.Date <= endDate.Date)
            {
                //Gets the next room in the list and then removes it from all rooms list         
                if (assignment == 1 && (roomList.First().DoubleRoom || doubleRoomsOnHold.Count > 0))
                {
                    RoomModel currentRoom;
                    if (doubleRoomsOnHold.Count > 0)
                    {
                        currentRoom = doubleRoomsOnHold.First();
                        doubleRoomsOnHold.Remove(currentRoom);
                    }
                    else
                    {
                        currentRoom = roomList.First();
                        roomList.Remove(currentRoom);
                    }

                    var washDayOne = new WashDayModel();
                    var washDayTwo = new WashDayModel();

                    //Lage vaskeoppgave 1 for parhybel
                    washDayOne.Date = currentDate.Date;
                    washDayOne.Assignment = 1;
                    washDayOne.RoomNumber = currentRoom.RoomNumber;
                    generatedWashList.Add(washDayOne);

                    //Vaskeoppgave to for parhybel samme dag
                    washDayTwo.Date = currentDate.Date;
                    washDayTwo.Assignment = 2;
                    washDayTwo.RoomNumber = currentRoom.RoomNumber;
                    generatedWashList.Add(washDayTwo);

                    currentDate = currentDate.AddDays(1);
                    continue;
                }
                else if (assignment == 2 && roomList.First().DoubleRoom)
                {
                    var currentRoom = roomList.First();
                    roomList.Remove(currentRoom);
                    doubleRoomsOnHold.Add(currentRoom);
                    continue;
                }
                else
                {   
                    var currentRoom = roomList.First();
                    roomList.Remove(currentRoom);
                    
                    var washDay = new WashDayModel();
                    washDay.Date = currentDate.Date;
                    washDay.Assignment = assignment;
                    washDay.RoomNumber = currentRoom.RoomNumber;
                    generatedWashList.Add(washDay);


                    if (assignment == 1)
                    {
                        assignment = 2;
                    }
                    else
                    {
                        assignment = 1;
                        currentDate = currentDate.AddDays(1);
                    }

                }
            }


            return generatedWashList;
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
                    errorList.Add($"Assignments not correctly numbered eg. not 1 and 2 on date {listToValidate[2*i].Date.ToString()}");
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

        public async Task<bool> BackupWashListToFile(List<WashDayModel> listToBackup)
        {
            string listAsString = "";
            foreach (var item in listToBackup)
            {
                listAsString += $"{item.Date.ToString()}\t{item.Assignment}\t{item.RoomNumber} \n\r";
            }

            StorageFolder backupFolder = ApplicationData.Current.LocalFolder;
            StorageFile backupFile = await backupFolder.CreateFileAsync(DateTime.Now.ToString());

            await Windows.Storage.FileIO.WriteTextAsync(backupFile, listAsString);


            return true;
        }
    }
}
