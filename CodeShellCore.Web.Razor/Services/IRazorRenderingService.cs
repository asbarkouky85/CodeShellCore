using CodeShellCore.Moldster.PageCategories.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Services
{
    public interface IRazorRenderingService
    {
        TemplateDataCollector GetCollector(HttpContext context, string viewName, string layout = null);
        string RenderPartial(HttpContext context, string viewName, object model = null, Dictionary<string, object> viewData = null);
    }
}
