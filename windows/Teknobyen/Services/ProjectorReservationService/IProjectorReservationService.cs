using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;

namespace Teknobyen.Services.ProjectorReservationService
{
    interface IProjectorReservationService
    {
        List<ProjectorReservationModel> GetReservations(bool syncAfterRetrieve);
        void SyncReservations();

        Task<bool> SaveReservation(ProjectorReservationModel reservationToSave);
        Task<bool> DeleteReservation(ProjectorReservationModel reservationToDelete);

    }
}
