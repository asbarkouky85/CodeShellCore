using CodeShellCore.Http;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Filters
{
    /// <summary>
    /// Uses query parameter "Token" to identify user, makes the call to <see cref="ISessionManager.UseToken(string)"/> then calls <see cref="IAuthorizationService.IsAuthorized(AuthorizationRequest)"/> where the <see cref="AuthorizationRequest"/> is filled from the route information and the <see cref="QueryAuthorizeFilter"/> instance itself
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]

    public class QueryAuthorizeFilter : CodeShellAuthorizeAttribute, IAsyncAuthorizationFilter
    {

        public QueryAuthorizeFilter()
        {

        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var tok = context.HttpContext.Request.Query["Token"];
                await context.HttpContext.ProcessOnce(tok);
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
