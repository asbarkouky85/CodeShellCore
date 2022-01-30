using CodeShellCore.Security;
using CodeShellCore.Security.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Filters
{
    public abstract class CodeShellAuthorizeAttribute : Attribute
    {
        public string[] Apps { get; set; }
        public string[] Clients { get; set; }
        public DefaultActions[] Actions { get; set; }
        public string Resource { get; set; }
        public string Action { get; set; }
        public bool AllowAnonymous { get; set; }
        public bool AllowAll { get; set; }

        public virtual void Authorize(AuthorizationFilterContext context)
        {

            if (AllowAnonymous)
                return;

            var _authService = context.HttpContext.RequestServices.GetService<IAuthorizationService>();

            if (_authService == null)
                return;
            
            if (AllowAll && _authService.IsLoggedIn)
                return;

            AuthorizationRequest<AuthorizationFilterContext> con = new AuthorizationRequest<AuthorizationFilterContext>(context);

            con.Resource = Resource ?? context.HttpContext.GetController();
            con.Action = Action ?? context.HttpContext.GetAction();
            con.Actions = Actions;
            con.Apps = Apps;
            con.Clients = Clients;

            if (!_authService.IsAuthorized(con))
                _authService?.OnUserIsUnauthorized(con);
        }
    }
}
