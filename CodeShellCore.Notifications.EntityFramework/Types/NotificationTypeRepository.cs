using CodeShellCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications.Types
{
    public class NotificationTypeRepository : KeyRepository<NotificationType, NotificationsContext, long>, INotificationTypeRepository
    {
        public NotificationTypeRepository(NotificationsContext con) : base(con)
        {
        }

        public async Task<List<NotificationType>> GetWithTemplates(List<long> types)
        {
            return await Loader.Include(e => e.Templates).Where(e => types.Contains(e.Id)).ToListAsync();
        }

        public async Task<NotificationType> GetWithProviders(long id)
        {
            var q = Loader.Include(e => e.Providers);
            return await q.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<NotificationType> GetWithTemplates(long notificationTypeId)
        {
            return await Loader.Include(e => e.Templates).Where(e => e.Id==notificationTypeId).FirstOrDefaultAsync();
        }
    }
}
