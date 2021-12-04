using CodeShellCore.Helpers;
using CodeShellCore.Text;
using CodeShellCore.Types;
using CodeShellCore.Web.Moldster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeShellCore.Web.Controllers
{
    public abstract class MoldsterUIController : Controller
    {

        public abstract string DefaultDomain { get; }
        public abstract string GetDefaultTitle(string loc);

        public virtual IServerConfig ServerConfig { get { return new DefaultServerConfig(); } }
        public virtual bool RestrictHttps => Shell.GetConfigAs<bool>("RestrictHttps",false);
        public virtual string HttpsUrl => Shell.GetConfigAs<string>("HttpsUrl", false);
        public virtual string[] Domains => Tenants.Select(d => d.Key).ToArray();
        public virtual Func<IndexModel, IActionResult> EmptyUrlHandler { get; }
        public virtual Dictionary<string, TenantInfoItem> Tenants
        {
            get
            {
                var _tenants = new Dictionary<string, TenantInfoItem>();
                string infoFile = Path.Combine(Shell.AppRootPath, "tenantInfo.json");
                if (System.IO.File.Exists(infoFile))
                {
                    _tenants = System.IO.File.ReadAllText(infoFile).FromJson<Dictionary<string, TenantInfoItem>>();
                }
                return _tenants;
            }
        }

        public string JsEnvironment
        {
            get
            {
                string env = Shell.GetConfigAs<string>("env", false);
                return (env == null || env == "production") ? "prod" : "dev";
            }
        }

        public virtual string RequestedDomain
        {
            get
            {
                string[] st = Request.Path.Value.Split('/');
                if (st.Length > 1 && !string.IsNullOrEmpty(st[1]))
                {
                    if (Domains.Contains(st[1]))
                        return st[1];
                }
                return DefaultDomain;
            }
        }


        public virtual IActionResult Index([FromRoute]string id, [FromQuery]LangConfig conf)
        {
            if (!Request.IsHttps && RestrictHttps)
            {
                string url = (HttpsUrl ?? "https://" + Request.Host) + Request.Path;
                return Redirect(url);
            }

            string dom = RequestedDomain;
            bool isDevBuild = JsEnvironment == "dev";

            TenantInfoItem info = null;

            if (Tenants.TryGetValue(dom, out TenantInfoItem item))
            {
                info = item;
            }

            string version = Shell.ProjectAssembly.GetVersionString();
            if (item != null && item.Version != null)
                version = item.Version;

            string loc = conf?.lang == null ? Request.GetLocaleFromCookie() : conf.lang;

            var mod = new IndexModel
            {
                Title = GetDefaultTitle(loc),
                Config = ServerConfig
            };

            mod.Config.Version = version;

            if (EmptyUrlHandler != null && Request.PathIsEmpty())
            {
                return EmptyUrlHandler.Invoke(mod);
            }

            mod.Config.Domain = dom;

            return IndexPage(mod);
        }

        protected virtual IActionResult IndexPage(IndexModel mod)
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
