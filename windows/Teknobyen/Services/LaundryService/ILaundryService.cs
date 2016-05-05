using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;

namespace Teknobyen.Services.LaundryService
{
    public interface ILaundryService
    {
        Task<ObservableCollection<LaundryMachineStatusModel>> GetMachineStatusList(string username, string password);
        Task<Double> GetAccountBalance(string username, string password);
    }
}
