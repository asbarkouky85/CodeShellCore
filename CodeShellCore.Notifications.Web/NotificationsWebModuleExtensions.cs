using CodeShellCore.Notifications.Devices;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Notifications
{
    public static class NotificationsWebModuleExtensions
    {
        public static void AddSignalRHub<TContract, THub>(this IServiceCollection coll) where THub : Hub<TContract> where TContract : class
        {
            coll.AddTransient<IEmitter<TContract>, SignalREmitter<THub, TContract>>();
        }

    }
}
