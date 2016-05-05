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


                foreach (var item in parsedJson)
                {
                    var tf = item.Value.ToObject<ReservationJsonModel>();
                    var t = new ProjectorReservationModel();
                    t.comment = tf.comment;
                    t.date = DateTime.Parse(tf.date);
                    t.roomNumber = int.Parse(tf.roomNumber);
                    t.startHour = DateTime.Parse(tf.startHour);
                    t.stopHour = DateTime.Parse(tf.stopHour);

                    returnObject.Add(t);
                }
            }
            return returnObject;
        }

    }
}
