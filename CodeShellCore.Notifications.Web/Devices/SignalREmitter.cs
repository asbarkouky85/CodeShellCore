using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications.Devices
{
    public class SignalREmitter<T, TContract> : IEmitter<TContract>
        where T : Hub<TContract>
        where TContract : class
    {
        protected IHubContext<T, TContract> Context;

        public SignalREmitter(IHubContext<T, TContract> con)
        {
            Context = con;
        }

        public virtual void Emit(Expression<Func<TContract, Task>> action, string[] only = null, string[] exclude = null)
        {
            var cl = Context.Clients.All;

            if (only != null)
                cl = Context.Clients.Clients(only);
            else if (exclude != null)
                cl = Context.Clients.AllExcept(exclude);

            var t = action.Compile().Invoke(cl);
            t.Wait();
        }

        public virtual async Task EmitAsync(Expression<Func<TContract, Task>> action, string[] only = null, string[] exclude = null)
        {
            var cl = Context.Clients.All;

            if (only != null)
                cl = Context.Clients.Clients(only);
            else if (exclude != null)
                cl = Context.Clients.AllExcept(exclude);

            await action.Compile().Invoke(cl);
        }
    }
}
