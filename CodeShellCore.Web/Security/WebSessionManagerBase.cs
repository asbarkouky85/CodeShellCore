using CodeShellCore.Extensions;
using CodeShellCore.MultiTenant;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        protected void ReadAppVersion()
        {
            var headers = _accessor.HttpContext?.Request?.Headers;
            if (headers != null && headers.ContainsKey(HttpHeaderKeys.AppVersion))
            {
                var ver = headers[HttpHeaderKeys.AppVersion];
                _accessor.HttpContext.RequestServices.SetCurrentTenantVersion(ver);
            }
        }

        protected virtual long? ReadTenantId()
        {
            var headers = _accessor.HttpContext?.Request?.Headers;
            if (headers != null && headers.TryGetValue(HttpHeaderKeys.TenantId, out StringValues tenantId) && long.TryParse(tenantId.First(), out long id))
            {
                ServiceProvider.GetRequiredService<CurrentTenant>().TenantId = id;
                return id;
            }
            return null;
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

        public abstract Task AuthorizationRequest();
        public abstract void UseToken(string token);
    }
}
