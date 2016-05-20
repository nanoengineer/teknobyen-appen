using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Messages;
using Teknobyen.Models;
using Teknobyen.Services.FirebaseService;
using Teknobyen.Services.StorageService;

namespace Teknobyen.Services.ProjectorReservationService
{
    class ProjectorReservationService : IProjectorReservationService
    {
        private IFirebaseService _firebaseService;


        public static ProjectorReservationService Instance {get; }
        static ProjectorReservationService() {
            Instance = Instance ?? new ProjectorReservationService();
        }
        private ProjectorReservationService()
        {
            _firebaseService = FirebaseService.FirebaseService.Instance;
        }


        public Task<bool> DeleteReservation(ProjectorReservationModel reservationToDelete)
        {
            throw new NotImplementedException();
        }

        public List<ProjectorReservationModel> GetReservations(bool syncAfterRetrieve)
        {
            using (var db = new ProjectorReservationContext())
            {
                var reservations = db.ProjectorReservations.OrderBy(e => e.startTime).ToList();
                if(syncAfterRetrieve) SyncReservations();
                return reservations;
            }
        }

        public Task<bool> SaveReservation(ProjectorReservationModel reservationToSave)
        {
            throw new NotImplementedException();
        }

        public async void SyncReservations()
        {
            bool madeChanges = false;
            try
            {
                var reservationsFromWeb = new ReservationListWrapper(await _firebaseService.GetReservations());
                using (var db = new ProjectorReservationContext())
                {
                    var localReservations = new ReservationListWrapper(db.ProjectorReservations.ToList());

                    //Remove reservations not on firebase
                    //Matching on all parameters
                    #region Remove reservations
                    var reservationsToBeRemoved = new List<ProjectorReservationModel>();
                    foreach (var item in localReservations.ReservationList)
                    {
                        if (!reservationsFromWeb.IsReservationInList(item))
                        {
                            reservationsToBeRemoved.Add(item);
                        }
                    }

                    if (reservationsToBeRemoved.Count() > 0)
                    {
                        db.ProjectorReservations.RemoveRange(reservationsToBeRemoved.ToArray());
                        madeChanges = true;
                    }
                    #endregion


                    //Add reservations to local storage, matching on all 
                    //parametst. This will ensure all changes are stored.
                    #region Add reservations

                    var reservationsToBeAdded = new List<ProjectorReservationModel>();
                    foreach (var item in reservationsFromWeb.ReservationList)
                    {
                        if (!localReservations.IsReservationInList(item))
                        {
                            reservationsToBeAdded.Add(item);
                        }
                    }
                    

                    if (reservationsToBeAdded.Count() > 0)
                    {
                        db.ProjectorReservations.AddRange(reservationsToBeAdded.ToArray());
                        madeChanges = true;
                    }
                    #endregion

                    if (madeChanges)
                    {
                        db.SaveChanges();
                    }
                    
                }
            }
            catch (Exception)
            {
                //Throw exception
            }

            if (madeChanges)
            {
                App.EventAggregator.GetEvent<ProjectorReservationsUpdated>().Publish("");
            }
        }
    }
}
