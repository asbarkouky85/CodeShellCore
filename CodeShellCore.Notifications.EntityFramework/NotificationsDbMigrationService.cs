using CodeShellCore.EntityFramework.Migrations;
using CodeShellCore.MultiTenant;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    public class NotificationsDbMigrationService : IMultiTenantDbMigrationService
    {
        private readonly NotificationsContext dbContext;

        public NotificationsDbMigrationService(NotificationsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void SetCurrentTenant(CurrentTenant tenant)
        {
            dbContext.SetCurrentTenant(tenant);
        }

        public async Task MigrateAsync()
        {
            await dbContext.Database.MigrateAsync();
        }
    }
}
