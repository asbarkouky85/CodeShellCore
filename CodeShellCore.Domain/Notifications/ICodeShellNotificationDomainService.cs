using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    public interface ICodeShellNotificationDomainService
    {
        Task CreateNotifications(NotificationCreateRequestData request);
    }
}