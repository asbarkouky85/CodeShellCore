using CodeShellCore.Security;
using CodeShellCore.Security.Sessions;
using CodeShellCore.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Security.Authorization;

namespace CodeShellCore.Web.Security
{
    public abstract class WebSessionManagerBase : SessionManagerBase
    {
        public WebSessionManagerBase(IUserDataService cache, IHttpContextAccessor context) : base(cache)
        {
            _accessor = context;
        }

        protected IHttpContextAccessor _accessor;

        public virtual void StartSession(IUser user, TimeSpan? length = null)
        {
            UserData.Save(user.UserId, user);
        }

        protected virtual void SetIdentity(object userId)
        {
            _accessor.HttpContext.User = new DefaultPrincipal(userId.ToString());
            var user = GetUserData();
            Shell.ScopedInjector.SetCurrentUser(user);
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

        protected bool IsProccessed(HttpContext con)
        {
            return con.Items.ContainsKey("IsProccessed");
        }

        protected void SetProccessed(HttpContext con)
        {
            con.Items["IsProccessed"] = true;
        }
    }
}
