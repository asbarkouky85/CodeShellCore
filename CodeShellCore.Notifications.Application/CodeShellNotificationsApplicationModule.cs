using CodeShellCore.Extensions.DependencyInjection;
using CodeShellCore.Modularity;
using CodeShellCore.Notifications.Devices;
using CodeShellCore.Notifications.Jobs;
using CodeShellCore.Notifications.Senders;
using CodeShellCore.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Notifications
{
    [DependsOn(
        typeof(CodeShellNotificationsApplicationContractsModule),
        typeof(CodeShellApplicationModule),
        typeof(CodeShellNotificationsDomainModule)
        )]
    public class CodeShellNotificationsApplicationModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddTransient<IDeviceService, DeviceService>();

            context.Services.AddTransient<INotificationsListService, NotificationListService>();
            context.Services.AddTransient<INotificationCreateService, NotificationCreateService>();
            context.Services.AddTransient<INotificationDeliveryService, NotificationDeliveryService>();
            context.Services.AddTransient<INotificationSender, ListNotificationSender>();
            context.Services.AddTransient<INotificationSender, EmailNotificationSender>();
            context.Services.AddTransient<INotificationSenderFactory, NotificationSenderFactory>();

            context.Services.AddTransient<IListNotificationSender, ListNotificationSender>();
            context.Services.AddAutoMapper(typeof(CodeShellNotificationsApplicationModule).Assembly);

            var jobs = context.Services.GetJobConfig();
            jobs.AddJobs(new[] { new FailedNotificationJob() });

            context.Services.Configure<CodeShellAppOptions>(e =>
            {
                e.UseJobs = true;
            });
        }

        public override void OnApplicationStarted(CodeShellApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(async () =>
            {
                await context.ServiceProvider.GetRequiredService<INotificationDeliveryService>().SendPendingMessages();
            });
        }
    }
}
