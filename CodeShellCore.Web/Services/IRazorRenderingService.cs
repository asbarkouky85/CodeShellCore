using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CodeShellCore.Web.Razor.Services
{
    public interface IRazorRenderingService
    {
        string RenderPartial(HttpContext context, string viewName, object model = null, Dictionary<string, object> viewData = null);
    }
}