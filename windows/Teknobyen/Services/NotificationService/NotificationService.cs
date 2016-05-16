using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Common;
using Teknobyen.Models;
using Windows.UI.Notifications;
using Teknobyen.Services.FirebaseService;
using Teknobyen.Services.SettingsService;
using Teknobyen.Services.WashListService;

namespace Teknobyen.Services.NotificationService
{
    class NotificationService : INotificationService
    {
        ISettingsService _settingsService;
        IWashListService _washlistService;
        IFirebaseService _firebaseService;


        public static NotificationService Instance { get; }
        static NotificationService()
        {
            Instance = Instance ?? new NotificationService();
        }

        private NotificationService()
        {
            _settingsService = SettingsService.SettingsService.Instance;
            _washlistService = WashListService.WashListService.Instance;
            _firebaseService = FirebaseService.FirebaseService.Instance;
        }


        public async void HandleNextWashDayNotification()
        {
            //Decide if there should be a notification
            var userRoom = RoomManager.GetRoomModel(_settingsService.RoomNumber);
            WashDayModel nextWashDay = await _firebaseService.GetNextWashDay(userRoom);

            if (nextWashDay != null)
            {
                //Means there should be 
                if (IsNotificationSet()) return;

                //Means no notification is set
                var notificationTime = new DateTime(nextWashDay.Date.Year, nextWashDay.Date.Month, nextWashDay.Date.Day, 16, 0, 0);
                CreateNotification(notificationTime, nextWashDay.Assignment);
                return;
            }
            else
            {
                RemoveNotifications();
            }
        }

        private void CreateNotification(DateTime dateAndTime, int assignment)
        {
            
            var dueTime = dateAndTime;
            var idNumber = dateAndTime.ToString("yyyyMMdd"); // Generates a unique ID number for the notification.
         
            // Set up the notification text.
            var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            var strings = toastXml.GetElementsByTagName("text");
            strings[0].AppendChild(toastXml.CreateTextNode("Husk at du har vaskedag i dag!"));
            strings[1].AppendChild(toastXml.CreateTextNode($"Du har oppgave {assignment}"));

            // Create the toast notification object.
            var toast = new Windows.UI.Notifications.ScheduledToastNotification(toastXml, dueTime);
            toast.Id = "TB" + idNumber;

            // Add to the schedule.
            Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);

        }

        private bool IsNotificationSet()
        {
            var notifier = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier();
            var scheduled = notifier.GetScheduledToastNotifications();

            if (scheduled.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void RemoveNotifications()
        {
            var notifier = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier();
            var scheduled = notifier.GetScheduledToastNotifications();

            foreach (var scheduledToast in scheduled)
            {
                notifier.RemoveFromSchedule(scheduledToast);
            }
        }

        
    }
}
