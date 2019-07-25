using CodeShellCore.Data.Helpers;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Services.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CodeShellCore.Web.Filters
{

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class ApiAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        

        public string Resource { get; set; }
        public string Action { get; set; }
        public bool AllowAnonymous { get; set; }

        private readonly IAuthorizationService _authorizationService;
        public ApiAuthorizeAttribute()
        {
            _authorizationService = Shell.ScopedInjector.GetService<IAuthorizationService>();
        }

        protected IAccessControlAuthorizationService AccessControl
        {
            get
            {
                if (_authorizationService is IAccessControlAuthorizationService)
                    return (IAccessControlAuthorizationService)_authorizationService;
                return null;
            }
        }

        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                _authorizationService?.SessionManager?.AuthorizationRequest();
                
                if (AccessControl == null || AllowAnonymous)
                {
                    return;
                }

                if (AccessControl != null)
                {
                    AuthorizationRequest<AuthorizationFilterContext> con = new AuthorizationRequest<AuthorizationFilterContext>(context);

                    con.Resource = Resource ?? context.HttpContext.GetController();
                    con.Action = Action ?? context.HttpContext.GetAction();

                    if (!AccessControl.IsAuthorized(con))
                        AccessControl?.OnUserIsUnauthorized(con);

                }
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
