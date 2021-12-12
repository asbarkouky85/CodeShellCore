using CodeShellCore.Moldster.PageCategories.Dtos;
using CodeShellCore.Web.Razor.Services;
using Microsoft.AspNetCore.Http;

namespace CodeShellCore.Moldster.Razor.Services
{
    public interface IMoldsterRazorService : IRazorRenderingService
    {
        TemplateDataCollector GetCollector(HttpContext httpContext, string viewPath, string layout);
    }
}
