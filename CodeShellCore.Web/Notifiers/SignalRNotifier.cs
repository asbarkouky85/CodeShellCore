using CodeShellCore.Services.Notifications;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Notifiers
{
    public class SignalRNotifier<T, TNotifier> : INotificationContext<TNotifier>
        where T : Hub<TNotifier>
        where TNotifier : class, INotifier

    {
        protected IHubContext<T, TNotifier> Context;

        public SignalRNotifier(IHubContext<T, TNotifier> con)
        {
            Context = con;
        }

        public void Run(Func<TNotifier, Task> action)
        {
            var t = action(Context.Clients.All);
            t.Wait();

        }
    }
}
