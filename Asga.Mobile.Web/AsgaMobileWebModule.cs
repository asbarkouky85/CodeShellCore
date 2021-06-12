using Asga.Mobile.Web.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Web;
using Asga.Mobile.Business;
using Microsoft.AspNetCore.Routing;

namespace Asga.Mobile
{
    public static class AsgaMobileWebModule
    {

        public static void AddAsgaMobileWeb(this IServiceCollection coll)
        {
            coll.AddSignalR();
            coll.AddSignalRHub<INotificationsPushingContract, NotificationsHub>();
        }

        /// <summary>
        /// .MapHub<NotificationsHub>("/notificationsHub")
        /// </summary>
        /// <param name="b"></param>
        public static void UseAsgaMobileHubs(this IEndpointRouteBuilder b)
        {
            b.MapHub<NotificationsHub>("/notificationsHub");
        }
    }
}
