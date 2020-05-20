using CodeShellCore.Security.Authorization;
using CodeShellCore.Web;
using CodeShellCore.Http;
using CodeShellCore.Web.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Sessions;

namespace CodeShellCore.Web.Services
{
    public abstract class AccessControlAuthorizationService : AuthorizationService, IAccessControlAuthorizationService
    {

        public AccessControlAuthorizationService(ISessionManager manager) : base(manager)
        {
        }

        public override bool IsAuthorized(AuthorizationRequest req)
        {
            return IsAuthorized((AuthorizationRequest<AuthorizationFilterContext>)req);
        }

        protected abstract bool IsAuthorized(AuthorizationRequest<AuthorizationFilterContext> req);

        public override void OnUserIsUnauthorized(AuthorizationRequest args)
        {
            onUserIsUnAuthorized((AuthorizationRequest<AuthorizationFilterContext>)args);
        }

        protected virtual void onUserIsUnAuthorized(AuthorizationRequest<AuthorizationFilterContext> args)
        {
            HttpResult res = new HttpResult
            {
                RequestUrl = args.Context.HttpContext.Request.GetFullUrl(),
                Method = args.Context.HttpContext.Request.Method
            };
            res.SetStatusCode(HttpStatusCode.Unauthorized);
            res.Message = "UnAuthorized";
            res.Data["creds"] = new
            {
                Token = (SessionManager as TokenSessionManager).GetTokenFromHeader(),
                DeviceId = args.Context.HttpContext.Request.GetDeviceIdFromCookie(),
                CurrentHost=args.Context.HttpContext.Request.GetHostUrl()
            };
            args.Context.Result = args.Context.Respond(res);
        }
    }
}
