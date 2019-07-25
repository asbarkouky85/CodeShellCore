using CodeShellCore.Web.Razor.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Razor.Services
{
    public interface IMoldsterRazorService : IRazorRenderingService
    {
        TemplateDataCollector GetCollector(HttpContext httpContext, string viewPath, string layout);
    }
}
