using CodeShellCore.Modularity;
using CodeShellCore.Notifications.Devices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
