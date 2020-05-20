using CodeShellCore.Security;
using CodeShellCore.Security.Sessions;
using CodeShellCore.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using CodeShellCore.Security.Authorization;
using Microsoft.Extensions.Primitives;

namespace CodeShellCore.Web.Security
{
    public abstract class WebSessionManagerBase : SessionManagerBase, ISessionManager
    {
        protected readonly IHttpContextAccessor _accessor;

        public virtual TimeSpan DefaultSessionTime => new TimeSpan(1, 0, 0, 0, 0);

        public WebSessionManagerBase(IHttpContextAccessor context)
        {
            _accessor = context;
        }

        public virtual void StartSession(IUser user, TimeSpan? length = null)
        {

        }

        protected virtual void SetIdentity(object userId)
        {
            _accessor.HttpContext.User = new DefaultPrincipal(userId.ToString());
            Shell.ScopedInjector.SetCurrentUserId(userId);
        }

        public void SetContextItem(string index, object value)
        {
            _accessor.HttpContext.Items[index] = value;
        }

        public object GetContextItem(string index)
        {
            object item;
            if (_accessor.HttpContext.Items.TryGetValue(index, out item))
                return item;
            return null;
        }

        public string GetDeviceId()
        {
            return _accessor.HttpContext?.Request?.GetDeviceIdFromCookie();
        }

        public override string GetConnectionId()
        {
            var head = _accessor.HttpContext?.Request?.Headers;
            if (head != null && head.TryGetValue(HttpHeaderKeys.ConnectionId, out StringValues val))
                return val;
            return null;
        }

        protected bool IsProccessed(HttpContext con)
        {
            return con.Items.ContainsKey("IsProccessed");
        }

        protected void SetProccessed(HttpContext con)
        {
            con.Items["IsProccessed"] = true;
        }

        public override void EndSession()
        {
            if (_accessor.HttpContext != null)
            {
                var ser = _accessor.HttpContext.RequestServices.GetService<IUserDataService>();
                ser.ClearUserData(GetCurrentUserId());
            }
        }

        public abstract bool IsLoggedIn();

        public abstract void AuthorizationRequest();

        public abstract void AuthorizationRequest(string token);
    }
}
