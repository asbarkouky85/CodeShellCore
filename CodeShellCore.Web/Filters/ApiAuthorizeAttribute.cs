using CodeShellCore.Data.Helpers;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Services.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using CodeShellCore.Security.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Web.Filters
{

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class ApiAuthorizeAttribute : Attribute, IAuthorizationFilter
    {


        public string Resource { get; set; }
        public string Action { get; set; }
        public bool AllowAnonymous { get; set; }
        public bool AllowAll { get; set; }

        public ApiAuthorizeAttribute()
        {

        }



        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                

                if (!context.HttpContext.IsProccessed())
                {
                    var _manager = context.HttpContext.RequestServices.GetService<ISessionManager>();
                    _manager?.AuthorizationRequest();
                }

                var _authorizationService = context.HttpContext.RequestServices.GetService<IAuthorizationService>();
                if (_authorizationService == null)
                    return;

                var _accessControl = (IAccessControlAuthorizationService)_authorizationService;

                if (_accessControl == null || AllowAnonymous)
                {
                    return;
                }

                if (AllowAll && _authorizationService.SessionManager.IsLoggedIn())
                    return;

                AuthorizationRequest<AuthorizationFilterContext> con = new AuthorizationRequest<AuthorizationFilterContext>(context);

                con.Resource = Resource ?? context.HttpContext.GetController();
                con.Action = Action ?? context.HttpContext.GetAction();

                if (!_accessControl.IsAuthorized(con))
                    _accessControl?.OnUserIsUnauthorized(con);
            }
            catch (Exception ex)
            {
                HttpResult res = new HttpResult
                {
                    Code = 500,
                    RequestUrl = context.HttpContext.Request.GetFullUrl(),
                    Method = context.HttpContext.Request.Method
                };
                res.SetException(ex);
                context.Result = context.Respond(res);
            }


        }



    }
}
