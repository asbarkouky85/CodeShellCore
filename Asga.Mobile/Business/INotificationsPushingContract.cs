using CodeShellCore.Services.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Asga.Mobile.Business
{
    public interface INotificationsPushingContract : IPushingContract
    {
        Task NotificationsChanged(int count);
    }
}
