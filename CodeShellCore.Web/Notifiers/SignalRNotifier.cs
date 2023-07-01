using CodeShellCore.Http.Pushing;
using CodeShellCore.Services.Notifications;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Notifiers
{
    public class SignalRNotifier<T, TContract> : IMessagePusher<TContract>
        where T : Hub<TContract>
        where TContract : class
    {
        protected IHubContext<T, TContract> Context;

        public SignalRNotifier(IHubContext<T, TContract> con)
        {
            Context = con;
        }

        public virtual void Publish(Func<TContract, Task> action, string[] only = null, string[] exclude = null)
        {
            var cl = Context.Clients.All;

            if (only != null)
                cl = Context.Clients.Clients(only);
            else if (exclude != null)
                cl = Context.Clients.AllExcept(exclude);

            var t = action(cl);
            t.Wait();
        }
    }
}
