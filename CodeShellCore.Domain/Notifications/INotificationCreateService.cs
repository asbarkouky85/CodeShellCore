using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    public interface INotificationCreateService
    {
        Task CreateNotifications(NotificationCreateRequestData request);
    }
}