using CodeShellCore.Data;
using CodeShellCore.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    public interface INotificationRepository : IKeyRepository<Notification, long>
    {
        Task<PagedResult<Notification>> GetByUser(long id, PagedListRequest opts);
        Task<PagedResult<NotificationMessage>> GetPendingMessages(PagedListRequest request);
        Task<PagedResult<NotificationMessage>> GetPendingRetryMessages(PagedListRequest request);
    }
}
