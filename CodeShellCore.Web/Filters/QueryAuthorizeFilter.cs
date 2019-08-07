using CodeShellCore.Security.Authorization;
using CodeShellCore.Services.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Filters
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class QueryAuthorizeFilter : Attribute, IAuthorizationFilter
    {
        public string Resource { get; set; }
        public string Action { get; set; }
        public bool AllowAnonymous { get; set; }
        protected readonly IAuthorizationService _authorizationService;
        protected readonly IAccessControlAuthorizationService _accessControl;
        public QueryAuthorizeFilter()
        {
            _authorizationService = Shell.ScopedInjector.GetService<IAuthorizationService>();
            if (_authorizationService is IAccessControlAuthorizationService)
                _accessControl = (IAccessControlAuthorizationService)_authorizationService;
        }

        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                if (_authorizationService == null)
                    return;

                var tok = context.HttpContext.Request.Query["Token"];
                string token = null;
                if (tok.Count > 0)
                    token = tok[0];
                _authorizationService.AuthorizationRequest(token);

                if (_accessControl == null || AllowAnonymous)
                {
                    return;
                }

                AuthorizationRequest<AuthorizationFilterContext> con = new AuthorizationRequest<AuthorizationFilterContext>(context);

                con.Resource = Resource ?? context.HttpContext.GetController();
                con.Action = Action ?? context.HttpContext.GetAction();

                if (!_accessControl.IsAuthorized(con))
                    _accessControl.OnUserIsUnauthorized(con);

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
