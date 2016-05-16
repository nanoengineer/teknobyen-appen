using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknobyen.Services.NotificationService
{
    interface INotificationService
    {
        void HandleNextWashDayNotification();
    }
}
