using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Teknobyen.Messages;
using Teknobyen.Models;
using Teknobyen.Services.StorageService;
using Windows.Security.Credentials;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace Teknobyen.Services.LaundryService
{
    public class LaundryService : ILaundryService
    {

        public static LaundryService Instance{get;}
        static LaundryService()
        {
            Instance = Instance ?? new LaundryService();
        }

        public LaundryBalanceModel GetAccountBalance(string username, string password, bool syncAfterRetrieve)
        {
            //Using messaging here as well
            if (syncAfterRetrieve)
            {
                GetAccountBalanceFromWeb(username, password);
            }

            //Returning last stored balance. Will be updated unless there is an internetproblem.
            using (var db = new LaundryBalanceContext())
            {
                if (db.Balance.Count() > 0)
                {
                    return db.Balance.OrderByDescending(e => e.Retrieved).First();
                }
                else
                {
                    //Being here means this is the first time. Only option is to return null and wait for
                    //sync from web to send message
                    return null;
                }
            }
        }

        private async void GetAccountBalanceFromWeb(string username, string password)
        {
            bool balanceUpdated = false;
            try
            {
                LaundryBalanceModel balance;

                using (var client = GetClient(new Uri("http://129.241.152.11"), username, password))
                {
                    var response = await client.GetAsync(new Uri("http://129.241.152.11/SaldoForm?lg=2&ly=9131"));
                    string htmlContent = await response.Content.ReadAsStringAsync();

                    var doc = new HtmlDocument();
                    doc.LoadHtml(htmlContent);

                    var s = doc.DocumentNode.Descendants("tr").Where(x => x.InnerText.Contains("BALANSE"));

                    var sList = new List<string>();
                    foreach (var item in s)
                    {
                        if (!item.InnerText.Contains("Kjøp"))
                        {
                            sList.Add(item.InnerText);
                        }
                    }

                    var _balanceString = sList.First();
                    _balanceString = _balanceString.Substring(7).Split(new char[] { ' ' }).First();

                    double _accountBalance = double.Parse(_balanceString);


                    balance = new LaundryBalanceModel(DateTime.Now, _accountBalance);
                    balanceUpdated = true;
                }

                if (balanceUpdated && balance != null)
                {
                    using (var db = new LaundryBalanceContext())
                    {
                        db.Balance.Add(balance);
                        db.SaveChanges();
                    }
                    App.EventAggregator.GetEvent<LaundryBalanceUpdated>().Publish("");
                }

            }
            catch (Exception)
            {
                //Log exception
            }
        }

        public async Task<ObservableCollection<LaundryMachineStatusModel>> GetMachineStatusList(string username, string password)
        {
            
            ObservableCollection<LaundryMachineStatusModel> machineList = new ObservableCollection<LaundryMachineStatusModel>();

            using (var client = GetClient(new Uri("http://129.241.152.11"), username, password))
            {
                var response = await client.GetAsync(new Uri("http://129.241.152.11/LaundryState?lg=2&ly=9131"));
                string htmlContent = await response.Content.ReadAsStringAsync();
                var doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);
                var s = doc.DocumentNode.Descendants("table").Where(x => x.LastChild.Name == "#text").Where(x => x.Attributes.Count == 5);

                foreach (var item in s)
                {
                    string ss = item.InnerText.Trim(); //Maskin 2Ledig
                    string status = ss.Substring(8); //Contains status Must be prossesed further
                    string number = ss.Substring(7,1); //Contains machine number

                    int mNum = 0;
                    bool sussess = int.TryParse(number, out mNum); //Throw exc if false

                    int minutesLeft = 0;
                    if (status == "Ledig")
                    {
                        minutesLeft = -1;
                    }
                    else
                    {
                        string minutesLeftString = Regex.Replace(status, "[^0-9]", "");
                        bool convertionSuccess = int.TryParse(minutesLeftString, out minutesLeft);
                    }

                    var machine = new LaundryMachineStatusModel(mNum, minutesLeft);
                    machineList.Add(machine);
                }
            }

            return machineList;
        }

        public async Task<bool> IsValidCredentials(string username, string password)
        {
            try
            {
                using (var client = GetClient(new Uri("http://129.241.152.11"), username, password))
                {
                    var response = await client.GetAsync(new Uri("http://129.241.152.11/LaundryState?lg=2&ly=9131"));
                    if (response.IsSuccessStatusCode)
                    {
                        SettingsService.SettingsService.Instance.IsLoggedInToLaundrySite = true;
                        return true;
                    }
                    else
                    {
                        SettingsService.SettingsService.Instance.IsLoggedInToLaundrySite = false;
                        return false;
                    }

                }
            }
            catch (Exception)
            {
                SettingsService.SettingsService.Instance.IsLoggedInToLaundrySite = false;
                return false;
            }
        }

        private HttpClient GetClient(Uri baseUri, string username, string password)
        {
            var filter = new HttpBaseProtocolFilter
            {
                AllowUI = false,
                CacheControl = { WriteBehavior = HttpCacheWriteBehavior.NoCache },
                ServerCredential =
                    new PasswordCredential(
                    baseUri.ToString(),
                    username,
                    password),
            };

            var httpClient = new HttpClient(filter);
            var headers = httpClient.DefaultRequestHeaders;
            var httpConnectionOptionHeaderValueCollection = headers.Connection;
            httpConnectionOptionHeaderValueCollection.Clear();
            headers.CacheControl.TryParseAdd("no-cache");
            headers.Add("Pragma", "no-cache");
            headers.Add("Keep-Alive", "false");
            headers.Cookie.Clear();

            return httpClient;
        }
    }
}
