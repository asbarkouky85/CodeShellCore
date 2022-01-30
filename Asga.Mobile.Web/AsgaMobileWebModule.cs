using Asga.Mobile.Web.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Web;
using Asga.Mobile.Business;

namespace Asga.Mobile
{
    public static class AsgaMobileWebModule
    {

        public static void AddAsgaMobileWeb(this IServiceCollection coll)
        {
            coll.AddSignalR();
            coll.AddSignalRHub<INotificationsPushingContract, NotificationsHub>();
        }
        public static void UseAsgaMobileSignalR(this IApplicationBuilder app)
        {
            app.UseSignalR(d =>
            {
                d.MapHub<NotificationsHub>("/notificationsHub");
            });
        }
    }
}
