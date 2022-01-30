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
    /// <summary>
    /// Makes the call to <see cref="ISessionManager.AuthorizationRequest()"/> then calls <see cref="IAuthorizationService.IsAuthorized(AuthorizationRequest)"/> where the <see cref="AuthorizationRequest"/> is filled from the route information and the <see cref="QueryAuthorizeFilter"/> instance itself
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class ApiAuthorizeAttribute : CodeShellAuthorizeAttribute, IAuthorizationFilter
    {
        


        public ApiAuthorizeAttribute()
        {

        }

        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                context.HttpContext.ProcessOnce();
                Authorize(context);
            }
            catch (Exception ex)
            {
                var res = context.HttpContext.HandleRequestError(ex);
                context.Result = new JsonResult(res);
            }
        }
    }
}
