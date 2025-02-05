using CodeShellCore.Extensions.DependencyInjection;
using CodeShellCore.Modularity;
using CodeShellCore.Notifications.Types;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Notifications
{
    [DependsOn(
        typeof(CodeShellNotificationsDomainModule))]
    public class CodeShellNotificationsEntityFrameworkModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddScoped<NotificationsUnit>();
            context.Services.AddScoped<INotificationsUnit, NotificationsUnit>();

            context.Services.AddRepositoryFor<Notification, NotificationRepository, INotificationRepository>();
            context.Services.AddRepositoryFor<NotificationType, NotificationTypeRepository, INotificationTypeRepository>();
            context.Services.AddMultiTenantDbMigrationsService<NotificationsDbMigrationService>();
            context.Services.AddCodeShellNotificationsEntityFramework<NotificationsContext>();
        }
    }
}
