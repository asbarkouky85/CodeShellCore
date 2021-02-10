using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using CodeShellCore.Http;
using CodeShellCore.Security;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Security.Authorization;

namespace CodeShellCore.Web.Filters
{

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class ApiAuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public DefaultActions[] Actions { get; set; }
        public string Resource { get; set; }
        public string Action { get; set; }
        public bool AllowAnonymous { get; set; }
        public bool AllowAll { get; set; }
        public bool IntegrationAction { get; set; }

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

                if (AllowAnonymous)
                    return;

                var _accessControl = context.HttpContext.RequestServices.GetService<IAuthorizationService>();

                if (_accessControl == null)
                {
                    return;
                }

                if (AllowAll && _accessControl.SessionManager.IsLoggedIn())
                    return;

                AuthorizationRequest<AuthorizationFilterContext> con = new AuthorizationRequest<AuthorizationFilterContext>(context);

                con.Resource = Resource ?? context.HttpContext.GetController();
                con.Action = Action ?? context.HttpContext.GetAction();
                con.Actions = Actions;
                con.IntegrationAction = IntegrationAction;

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
