using CodeShellCore.Web.Filters;
using CodeShellCore.Web.Moldster;
using CodeShellCore.Web.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Controllers
{
    [QueryAuthorizeFilter(AllowAnonymous = true)]
    public abstract class SpaHomeController : BaseMvcController
    {
        protected virtual bool UseLegacy { get; }

        public virtual string DefaultTenant => Shell.GetConfigAs<string>("DefaultTenant", false);
        public virtual bool RestrictHttps => Shell.GetConfigAs<bool>("RestrictHttps", false);
        public virtual string HttpsUrl => Shell.GetConfigAs<string>("HttpsUrl", false);
        public abstract string GetDefaultTitle(string loc);

        public async virtual Task Index()
        {
            if (!Request.IsHttps && RestrictHttps)
            {
                string url = (HttpsUrl ?? "https://" + Request.Host) + Request.Path;
                HttpContext.Response.Redirect(url);
            }
            else
            {
                var service = GetService<ISpaFallbackHandler>();
                await service.HandleRequestAsync(HttpContext);
            }
        }

        protected virtual ViewResult IndexPage(IndexModel mod)
        {
            return View("~/Pages/Index.cshtml", mod);
        }

        public virtual IActionResult SetLocale(string lang)
        {
            Response.SetLocaleCookie(lang);
            return Json(new { });
        }
    }
}
