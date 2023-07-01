using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Web.Filters
{
    /// <summary>
    /// Uses query parameter "Token" to identify user, makes the call to <see cref="ISessionManager.AuthorizationRequest(string)"/> then calls <see cref="IAuthorizationService.IsAuthorized(AuthorizationRequest)"/> where the <see cref="AuthorizationRequest"/> is filled from the route information and the <see cref="QueryAuthorizeFilter"/> instance itself
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    
    public class QueryAuthorizeFilter : CodeShellAuthorizeAttribute, IAuthorizationFilter
    {

        public QueryAuthorizeFilter()
        {

        }

        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {

                var tok = context.HttpContext.Request.Query["Token"];
                context.HttpContext.ProcessOnce(tok);
                Authorize(context);
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
