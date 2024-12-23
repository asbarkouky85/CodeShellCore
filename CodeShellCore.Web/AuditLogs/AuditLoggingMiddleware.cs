using CodeShellCore.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.AuditLogs
{
    public class AuditLoggingMiddleware : IMiddleware
    {
        public AuditLoggingMiddleware()
        {
            AuditLogger.Initialize();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            using (var watch = SW.Measure())
            {
                var ignore = context.Request.Path != null && context.Request.Path == "/health";
                if (!ignore)
                    AuditLogger.Log.WriteLine($"{context.Request.Method} {context.Request.Path}");
                await next.Invoke(context);
                if (!ignore)
                    AuditLogger.Log.WriteLine($"{context.Request.Method} {context.Request.Path} - ({context.Response.StatusCode}) ({watch.Elapsed.TotalMilliseconds})");

            }
        }
    }
}
