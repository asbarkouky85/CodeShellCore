using Asga.Mobile.Business;
using CodeShellCore.Data.Helpers;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Web.Notifiers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asga.Mobile.Web.Hubs
{
    public class NotificationsHub : SignalRHub<INotificationsPushingContract>
    {

        public NotificationsHub()
        {

        }
        public string SetUserConnectionId(string userId, string deviceId, string lang)
        {
            using (var sc = CodeShellCore.Shell.GetScope())
            {
                var conn = GetConnectionId();
                var service = sc.ServiceProvider.GetService<IUserDeviceService>();
                SubmitResult res = service.UpdateUserDevice(userId, DeviceType.SignalR, deviceId, conn, lang);
                return conn;
            }

        }

        public void ClearUserConnectionId(string userId, string deviceId)
        {
            using (var sc = CodeShellCore.Shell.GetScope())
            {
                var conn = GetConnectionId();
                var service = sc.ServiceProvider.GetService<IUserDeviceService>();
                SubmitResult res = service.ClearDeviceConnection(userId,deviceId);

            }
        }
    }
}
