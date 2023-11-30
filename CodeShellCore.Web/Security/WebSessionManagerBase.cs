﻿using CodeShellCore.Security;
using CodeShellCore.Security.Sessions;
using CodeShellCore.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using CodeShellCore.Security.Authorization;
using Microsoft.Extensions.Primitives;
using CodeShellCore.Security.Authentication;

namespace CodeShellCore.Web.Security
{
    public abstract class WebSessionManagerBase : SessionManagerBase, ISessionManager
    {
        protected readonly IHttpContextAccessor _accessor;
        protected IServiceProvider ServiceProvider { get; private set; }

        public virtual TimeSpan DefaultSessionTime => new TimeSpan(1, 0, 0, 0, 0);

        public WebSessionManagerBase(IServiceProvider prov) : base(prov)
        {
            ServiceProvider = prov;
            _accessor = prov.GetService<IHttpContextAccessor>();
        }

        public virtual void StartSession(IUser user, TimeSpan? length = null)
        {

        }

        protected virtual void SetIdentity(JWTData jwt)
        {
            var id = jwt.UserId;
            if (!string.IsNullOrEmpty(jwt.TenantId) && long.TryParse(jwt.TenantId, out long tenantId))
            {
                id = jwt.TenantId + "_" + jwt.UserId;
                ServiceProvider.SetCurrentTenant(tenantId);
            }
            ServiceProvider.SetCurrentUserId(id);
            _accessor.HttpContext.User = new DefaultPrincipal(id);
        }

        protected virtual void SetIdentity(ClientJwt jwt)
        {
            _accessor.HttpContext.User = new DefaultPrincipal(jwt.ClientId);
            ServiceProvider.SetCurrentUserId(jwt.ClientId, true);
            if (!string.IsNullOrEmpty(jwt.TenantId) && long.TryParse(jwt.TenantId, out long tenantId))
            {
                ServiceProvider.SetCurrentTenant(tenantId);
            }
        }

        public virtual void SetContextItem(string index, object value)
        {
            _accessor.HttpContext.Items[index] = value;
        }

        public virtual object GetContextItem(string index)
        {
            object item;
            if (_accessor.HttpContext.Items.TryGetValue(index, out item))
                return item;
            return null;
        }

        public override string GetConnectionId()
        {
            var head = _accessor.HttpContext?.Request?.Headers;
            if (head != null && head.TryGetValue(HttpHeaderKeys.ConnectionId, out StringValues val))
                return val;
            return null;
        }

        public override string GetCurrentUserId()
        {
            return _accessor.HttpContext?.User?.Identity.Name;
        }

        public override void EndSession()
        {
            if (_accessor.HttpContext != null)
            {
                var ser = _accessor.HttpContext.RequestServices.GetService<IUserDataService>();
                ser.ClearUserData(GetCurrentUserId());
            }
        }

        public virtual bool IsLoggedIn()
        {
            var user = GetCurrentUserId();
            return !string.IsNullOrEmpty(user);
        }

        public abstract void AuthorizationRequest();

        public abstract void AuthorizationRequest(string token);
    }
}
