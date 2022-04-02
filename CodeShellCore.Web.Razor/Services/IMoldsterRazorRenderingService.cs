using CodeShellCore.Moldster.PageCategories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Services
{
    public interface IMoldsterRazorRenderingService : IRazorRenderingService
    {
        TemplateDataCollector GetCollector(HttpContext context, string viewName, string layout = null);
        
    }
}
