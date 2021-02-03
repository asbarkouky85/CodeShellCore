using CodeShellCore.Http;
using CodeShellCore.Files.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System;
using CodeShellCore.Cli;

namespace CodeShellCore.Web.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext actionExecutedContext)
        {
            var excep = actionExecutedContext.Exception;

            actionExecutedContext.Result = new JsonResult(actionExecutedContext.HttpContext.HandleRequestError(excep));

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
