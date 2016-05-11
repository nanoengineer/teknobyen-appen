using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Services.FirebaseService;
using Template10.Mvvm;

namespace Teknobyen.ViewModels
{
    class ProjectorReservationViewModel : ViewModelBase
    {
        IFirebaseService _firebaseService;




        //var p = CredentialsService.Instance.GetUser();

        //var m = new ProjectorReservationModel();
        //m.userId = p.UserName;
        //m.comment = "Game of Thrones";
        //m.name = SettingsService.Instance.Name;
        //m.roomNumber = SettingsService.Instance.RoomNumber;
        //m.date = new DateTime(2016, 05, 16);
        //m.startTime = new DateTime(2016, 05, 16, 07, 00, 0);
        //m.endTime = m.startTime.Add(new TimeSpan(1, 0, 0));
        //await _firebaseService.SaveReservation(m);
    }
}
