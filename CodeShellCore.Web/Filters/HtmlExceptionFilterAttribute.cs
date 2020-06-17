using CodeShellCore.Http;
using CodeShellCore.Files.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;

namespace CodeShellCore.Web.Filters
{
    public class HtmlExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private string GetMessage(Result res)
        {
            var temp = "<p style=\"color:#900\">" + res.Message + " (" + res.ExceptionMessage + ") " + "</p>";
            if (res.InnerResult != null)
            {
                temp += GetMessage(res.InnerResult);
            }
            return temp;
        }
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
            res.SetException(actionExecutedContext.Exception);

            if (actionExecutedContext.Exception is CodeShellHttpException)
                actionExecutedContext.HttpContext.Response.StatusCode = (int)((CodeShellHttpException)actionExecutedContext.Exception).Status;
            else
                actionExecutedContext.HttpContext.Response.StatusCode = 500;

            string template = "<title>Error</title>" +
"<body style=\"font-family: courier\">" +
"<h1>Error</h1>" +
"<em>{0}</em>" +
"{1}" +
"<p style=\"white-space:pre-wrap;font-size:12px;color:#333\">{2}</p>" +
"</body>";
            string fullError = GetMessage(res);
            string message = string.Format(template, fullError, res.ExceptionMessage, string.Join("\n", res.StackTrace));

            var s = new ContentResult();
            s.Content = message;
            s.ContentType = "text/html";
            actionExecutedContext.Result = s;
            Logger.WriteException(actionExecutedContext.Exception);
        }
    }
}
