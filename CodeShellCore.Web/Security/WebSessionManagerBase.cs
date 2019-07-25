using CodeShellCore.Security;
using CodeShellCore.Security.Sessions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Security
{
    public abstract class WebSessionManagerBase : SessionManagerBase
    {
        protected IHttpContextAccessor Accessor { get { return Shell.ScopedInjector.GetRequiredService<IHttpContextAccessor>(); } }
        
        protected virtual void SetIdentity(object userId)
        {
            Accessor.HttpContext.User = new DefaultPrincipal(userId.ToString());
        }

        public void SetContextItem(string index, object value)
        {
            Accessor.HttpContext.Items[index] = value;
        }

        public object GetContextItem(string index)
        {
            object item;
            if (Accessor.HttpContext.Items.TryGetValue(index, out item))
                return item;
            return null;
        }

        public string GetDeviceId()
        {
            return Accessor.HttpContext?.Request?.GetDeviceIdFromCookie();
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
