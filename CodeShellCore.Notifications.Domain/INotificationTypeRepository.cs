using CodeShellCore.Data;
using CodeShellCore.Notifications.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    public interface INotificationTypeRepository : IKeyRepository<NotificationType, long>
    {
        Task<List<NotificationType>> GetWithTemplates(List<long> types);
        Task<NotificationType> GetWithProviders(long id);
        Task<NotificationType> GetWithTemplates(long notificationTypeId);
    }
}