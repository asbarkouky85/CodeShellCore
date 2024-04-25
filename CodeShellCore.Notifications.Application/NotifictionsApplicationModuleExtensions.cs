using CodeShellCore.Notifications.Devices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Notifications
{
    public static class NotifictionsApplicationModuleExtensions
    {
        public static void AddCodeShellNotificationsApplication(this IServiceCollection services)
        {
            services.AddTransient<IDeviceService, DeviceService>();
        }
    }
}
