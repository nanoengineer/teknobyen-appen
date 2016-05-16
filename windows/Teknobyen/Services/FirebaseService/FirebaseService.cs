using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;
using Windows.Web.Http;

namespace Teknobyen.Services.FirebaseService
{
    class FirebaseService : IFirebaseService
    {
        public static FirebaseService Instance { get; }
        static FirebaseService()
        {
            // implement singleton pattern
            Instance = Instance ?? new FirebaseService();
        }

        private string base_uri = "https://teknobyen.firebaseio.com";
        private string client_secret = "5bfmWJnAb6gTwLpnrrDLqSY5c2LQPs88agx3WW2a";

        public async Task<List<ProjectorReservationModel>> GetReservations()
        {
            var returnObject = new List<ProjectorReservationModel>();


            string uri = $"{base_uri}/reservations.json?auth={client_secret}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(new Uri(uri));
                var s = await response.Content.ReadAsStringAsync();

                JObject parsedJson = JObject.Parse(s);

                var rm = JsonConvert.DeserializeObject<Dictionary<string, ReservationJsonModel>>(s);

                foreach (var item in rm)
                {
                    try
                    {
                        var reservation = new ProjectorReservationModel(item.Key, item.Value);
                        returnObject.Add(reservation);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("deserializing failed" + e.Message);
                    }
                }
            }
            return returnObject;
        }

        public async Task<List<WashDayModel>> GetWashList()
        {
            List<WashDayModel> listOfWashDays = new List<WashDayModel>();
            string uri = $"{base_uri}/washdays.json?auth={client_secret}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(new Uri(uri));
                var jsonString = await response.Content.ReadAsStringAsync();

                Dictionary<string, WashDayJsonModel> washDayList = JsonConvert.DeserializeObject<Dictionary<string, WashDayJsonModel>>(jsonString);

                foreach (var item in washDayList)
                {
                    try
                    {
                        var washDayModel = new WashDayModel(item.Key, item.Value);
                        listOfWashDays.Add(washDayModel);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine($"Deserialization of washday failed");
                    }
                }
            }

            return listOfWashDays.OrderBy(e => e.Date).ThenBy(e => e.Assignment).ToList();

        }


        //Using https://www.firebase.com/docs/rest/guide/saving-data.html#section-post as guide
        public async Task<bool> SaveReservation(ProjectorReservationModel reservation)
        {
            if (SettingsService.SettingsService.Instance.IsLoggedInToLaundrySite)
            {
                string username = CredentialsService.CredentialsService.Instance.GetUser().UserName;
                int roomnumber = SettingsService.SettingsService.Instance.RoomNumber;

                reservation.userId = username;
                reservation.roomNumber = roomnumber;

                string uri = $"{base_uri}/reservations.json?auth={client_secret}";

                using (var client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(uri));

                    var jsonModel = new ReservationJsonModel(reservation);
                    var content = JsonConvert.SerializeObject(jsonModel);
                    HttpStringContent stringContent = new HttpStringContent(content);

                    request.Content = stringContent;

                    var response = await client.SendRequestAsync(request);
                }
                return true;
            }

            return false;
            
       }

        public async Task<bool> SaveWashDayEntry(WashDayModel washDay)
        {
            bool success = false;
            string uri = $"{base_uri}/washdays.json?auth={client_secret}";

            try
            {
                using (var client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(uri));

                    //Serialize content
                    var serializedContent = JsonConvert.SerializeObject(new WashDayJsonModel(washDay));
                    HttpStringContent content = new HttpStringContent(serializedContent);
                    request.Content = content;
                    var s = await client.SendRequestAsync(request);

                    if (s.StatusCode != HttpStatusCode.Ok)
                    {
                        success = false;
                    }
                    else
                    {
                        success = true;
                    }
                }
            }
            catch (Exception)
            {
                //TODO: log e to hockeyapp
            }

            return success;
        }

        public async Task<bool> UpdateWashDayEntry(WashDayModel washDay)
        {
            bool success = false;
            if (String.IsNullOrWhiteSpace(washDay.FBID))
            {
                return false;
            }
            string uri = $"{base_uri}/washdays/{washDay.FBID}.json?auth={client_secret}";

            try
            {
                using (var client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, new Uri(uri));

                    //Serialize content
                    var serializedContent = JsonConvert.SerializeObject(new WashDayJsonModel(washDay));
                    HttpStringContent content = new HttpStringContent(serializedContent);
                    request.Content = content;
                    var s = await client.SendRequestAsync(request);

                    if (s.StatusCode != HttpStatusCode.Ok)
                    {
                        success = false;
                    }
                    else
                    {
                        success = true;
                    }
                }
            }
            catch (Exception)
            {
                //TODO: log e to hockeyapp
            }

            return success;
        }

        public async Task<WashDayModel> GetNextWashDay(RoomModel room)
        {
            try
            {
                var washList = await GetWashList();
                var nextWashDay = (from n in washList
                                   where n.RoomNumber == room.RoomNumber
                                   select n).ToList();
                if (nextWashDay.Count == 0)
                {
                    return null;
                }
                else
                {
                    return nextWashDay.OrderBy(e => e.Date).ThenBy(e => e.Assignment).First();
                }
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public async Task<bool> SaveWashDayEntries(List<WashDayModel> washDayList)
        {
            bool success = false;

            var currentList = await GetWashList();

            foreach (var washDay in washDayList)
            {
                var entriesForWashDayDate = (from m in currentList
                              where m.Date.Date == washDay.Date.Date
                              select m).ToList();
                if (entriesForWashDayDate.Count == 0)
                {
                    await SaveWashDayEntry(washDay);
                    continue;
                }
                else if (entriesForWashDayDate.Count == 1)
                {
                    if (entriesForWashDayDate.First().Assignment != washDay.Assignment)
                    {
                        await SaveWashDayEntry(washDay);
                        continue;
                    }
                }
                else
                {
                    await UpdateWashDayEntry(washDay);
                }

            }

            success = true;
            return success;
        }
    }
}
