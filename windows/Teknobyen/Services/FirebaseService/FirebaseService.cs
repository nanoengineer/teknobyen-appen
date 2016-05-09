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

                //var rm = JsonConvert.DeserializeObject<Dictionary<string, ReservationJsonModel>>(s);

                foreach (var item in parsedJson)
                {
                    try
                    {
                    var tf = item.Value.ToObject<ReservationJsonModel>();
                    var t = new ProjectorReservationModel();
                    t.comment = tf.comment;
                    t.date = DateTime.Parse(tf.date);
                    t.roomNumber = int.Parse(tf.roomNumber);
                    t.startHour = DateTime.Parse(tf.startHour);
                    t.startHour = new DateTime(t.date.Year, t.date.Month, t.date.Day, t.startHour.Hour, t.startHour.Minute, 0);
                    t.stopHour = DateTime.Parse(tf.stopHour);

                    returnObject.Add(t);
                    }
                    catch (Exception)
                    {
                        System.Diagnostics.Debug.WriteLine("deserializing failed");
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
            string uri = $"{base_uri}/reservations.json?auth={client_secret}";

            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(uri));

                var jsonModel = new ReservationJsonModel();
                jsonModel.comment = reservation.comment;
                jsonModel.date = reservation.date.ToString("dd.MM.yyyy");
                jsonModel.roomNumber = reservation.roomNumber.ToString();
                jsonModel.startHour = reservation.startHour.ToString("HH:mm");
                jsonModel.stopHour = reservation.stopHour.ToString("HH:mm");

                var content = JsonConvert.SerializeObject(jsonModel);
                HttpStringContent stringContent = new HttpStringContent(content);

                request.Content = stringContent;

                var response = await client.SendRequestAsync(request);
            }
            return true;
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
            catch (Exception e)
            {
                //TODO: log e to hockeyapp
            }

            return success;
        }
    }
}
