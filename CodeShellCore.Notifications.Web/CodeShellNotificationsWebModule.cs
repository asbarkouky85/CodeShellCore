using CodeShellCore.Modularity;
using CodeShellCore.Notifications.Devices;
using CodeShellCore.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using CodeShellCore.Notifications.Pushing;

namespace CodeShellCore.Notifications
{
    [DependsOn(
        typeof(CodeShellApplicationContractsModule),
        typeof(CodeShellWebModule))]
    public class CodeShellNotificationsWebModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddSignalR();
            context.Services.AddTransient<IDeviceService, NullDeviceService>();
            context.Services.AddSignalRHub<INotificationsPushingContract, NotificationsHub>();
            
        }

        public override void Configure(CodeShellApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationsHub>("/hubs/notificationsHub");
            });
        }
    }
}
