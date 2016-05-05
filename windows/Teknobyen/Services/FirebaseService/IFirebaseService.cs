using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;

namespace Teknobyen.Services.FirebaseService
{
    interface IFirebaseService
    {
        Task<List<ProjectorReservationModel>> GetReservations();
    }
}
