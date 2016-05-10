using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknobyen.Services.SettingsService
{
    interface ISettingsService
    {
        int RoomNumber { get; set; }
        string Name { get; set; }
        bool IsAdmin { get; set; }
        bool IsLoggedInToLaundrySite { get; set; }
    }
}
