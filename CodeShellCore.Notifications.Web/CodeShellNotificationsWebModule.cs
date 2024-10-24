using CodeShellCore.Modularity;
using CodeShellCore.Notifications.Devices;
using CodeShellCore.Web;
using Microsoft.Extensions.DependencyInjection;

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
            
        }
    }
}
