using CodeShellCore.Services.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace CodeShellCore.Web.Notifiers
{
    public class SignalRHub<T> : Hub<T> where T :class 
    {
        public virtual string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
