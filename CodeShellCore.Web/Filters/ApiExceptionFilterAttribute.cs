using CodeShellCore.Http;
using CodeShellCore.Files.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System;
using CodeShellCore.CLI;

namespace CodeShellCore.Web.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext actionExecutedContext)
        {
            HttpRequest req = actionExecutedContext.HttpContext.Request;
            string url = req.GetFullUrl();
            HttpResult res = new HttpResult
            {
                Code = 1,
                RequestUrl = url,
                Method = req.Method
            };
            var excep = actionExecutedContext.Exception;
            res.SetException(actionExecutedContext.Exception);

            if (excep is CodeShellHttpException)
                actionExecutedContext.HttpContext.Response.StatusCode = GetStatusCode((CodeShellHttpException)excep);
            else if (excep is ArgumentOutOfRangeException)
                actionExecutedContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            else if (excep is ArgumentException)
                actionExecutedContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            else
                actionExecutedContext.HttpContext.Response.StatusCode = 500;

            actionExecutedContext.Result = new JsonResult(res);
            Logger.WriteException(actionExecutedContext.Exception);

            var outService = actionExecutedContext.HttpContext.RequestServices.GetService<IOutputWriter>();
            if (outService != null)
            {
                using (outService.Set(ConsoleColor.Red))
                {
                    outService.WriteLine(actionExecutedContext.Exception.GetMessageRecursive());
                }

                using (outService.Set(ConsoleColor.DarkRed))
                {
                    outService.WriteLine(actionExecutedContext.Exception.StackTrace);
                }
            }
        }

        private int GetStatusCode(CodeShellHttpException ex)
        {
            return (int)ex.Status;
        }

    }
}
