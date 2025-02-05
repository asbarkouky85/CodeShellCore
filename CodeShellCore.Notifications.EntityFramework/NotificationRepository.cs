using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Extensions.DependencyInjection;
using CodeShellCore.Linq;
using CodeShellCore.Security;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    public class NotificationRepository : KeyRepository<Notification, NotificationsContext, long>, INotificationRepository
    {
        public NotificationRepository(NotificationsContext con) : base(con)
        {
        }


        public async Task<PagedResult<Notification>> GetByUser(long id, PagedListRequest opts)
        {
            var o = opts.GetOptionsFor<Notification>();

            var q = Loader;//;
            if (!string.IsNullOrEmpty(o.SearchTerm))
            {
                q = q.Where(d => d.SenderName.Contains(opts.SearchTerm) || d.NotificationType.Name.Contains(opts.SearchTerm) || d.Parameters.Contains(opts.SearchTerm));
            }
            var qSorted = q.Where(d =>
                d.UserId == id &&
                !d.IsRead &&
                d.NotificationMessages.Any(e => e.NotificationProviderId == NotificationProviders.List)
            ).OrderBy(d => d.IsRead).ThenByDescending(d => d.CreatedOn);
            return await qSorted.ToPagedResultAsync(o);
        }

        public async Task<PagedResult<NotificationMessage>> GetPendingMessages(PagedListRequest request)
        {
            var req = request.GetOptionsFor<NotificationMessage>();
            var q = DbContext.Set<NotificationMessage>().AsQueryable().AsSplitQuery();
            q = q.Include(e => e.Notification.User)
                .Include(e => e.NotificationProvider);
            return await q.Where(e => e.SendingStatusId == NotificationSendingStatus.New).ToPagedResultAsync(req);
        }

        public async Task<PagedResult<NotificationMessage>> GetPendingRetryMessages(PagedListRequest request)
        {
            var req = request.GetOptionsFor<NotificationMessage>();
            var q = DbContext.Set<NotificationMessage>().AsQueryable().AsSplitQuery();
            q = q.Include(e => e.Notification.User)
                .Include(e => e.NotificationProvider);
            return await q.Where(e => e.SendingStatusId == NotificationSendingStatus.Queued || e.SendingStatusId == NotificationSendingStatus.NoDevices)
                .ToPagedResultAsync(req);
        }
    }
}
