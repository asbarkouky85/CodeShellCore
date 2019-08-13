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
        public virtual bool RestrictHttps { get; }
        public virtual string[] Domains { get; }
        public virtual Func<IndexModel, IActionResult> EmptyUrlHandler { get; }

        public string JsEnvironment
        {
            get
            {
                string env = Shell.GetConfigAs<string>("env", false);
                return (env == null || env == "production") ? "prod" : "dev";
            }
        }

        public virtual string DomainName
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


        public IActionResult Index([FromRoute]string id, [FromQuery]LangConfig conf)
        {
            if (!Request.IsHttps && RestrictHttps)
            {
                return Redirect("https://" + Request.Host + Request.Path);
            }

            string dom = DomainName;
            bool isDevBuild = JsEnvironment == "dev";
            string loc = conf?.lang == null ? Request.GetLocaleFromCookie() : conf.lang;
            string ver = isDevBuild ? "dev" : "v" + Shell.ProjectAssembly.GetVersionString();
            string path = Path.Combine(Shell.AppRootPath, Shell.PublicRoot, "dist", ver);
            string search = isDevBuild ? "dev*.js" : dom + "-" + ver + "*.js";
            Utils.CreateFolderForFile(path + "\\n.x");
            string[] files = Directory.GetFiles(path, search);

            var mod = new IndexModel
            {
                Title = GetDefaultTitle(loc),
                Config = ServerConfig
            };
            mod.Config.Locale = loc;
            mod.Config.Env = JsEnvironment;
            mod.Config.Version = ver;
            mod.Config.Urls = Shell.GetConfigAs<Dictionary<string, string>>("Services", false);

            if (EmptyUrlHandler != null && Request.PathIsEmpty())
            {
                return EmptyUrlHandler.Invoke(mod);
            }

            mod.Config.Domain = dom;
            mod.Config.BaseURL = dom == DefaultDomain ? "/" : "/" + dom;
            mod.Config.Hash = isDevBuild ? "" : "~7d078181";
            mod.Chunks = files.Select(d => d.GetAfterLast("\\")).ToArray();

            return View("~/Pages/Index.cshtml", mod);
        }

        public IActionResult SetLocale(string lang)
        {
            Response.SetLocaleCookie(lang);
            return Json(new { });
        }
    }
}
