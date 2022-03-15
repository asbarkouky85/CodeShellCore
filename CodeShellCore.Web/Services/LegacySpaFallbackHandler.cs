using CodeShellCore.Web.Moldster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeShellCore.Text;
using System.Threading.Tasks;
using System.IO;
using CodeShellCore.Types;
using CodeShellCore.Helpers;

namespace CodeShellCore.Web.Services
{
    public class LegacySpaFallbackHandler
    {
        private readonly string defaultTenant;

        HttpRequest Request { get; set; }
        public LegacySpaFallbackHandler(HttpRequest req, string defaultTenant)
        {
            Request = req;
            this.defaultTenant = defaultTenant;
        }

        public virtual IServerConfig ServerConfig { get { return new DefaultServerConfig(); } }
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
                return defaultTenant;
            }
        }


        public virtual IndexModel Index(string defaultTitle)
        {
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

            string loc = Request.GetLocaleFromCookie();
            string package = isDevBuild ? "dev" : "v" + version;
            string path = Path.Combine(Shell.AppRootPath, Shell.PublicRoot, "dist", package);
            string search = isDevBuild ? "dev*.js" : dom + "-" + package + "*.js";
            Utils.CreateFolderForFile(path + "\\n.x");
            string[] files = Directory.GetFiles(path, search);

            var mod = new IndexModel
            {
                Title = defaultTitle,
                Config = ServerConfig,
                PackageId = package
            };
            mod.Config.Locale = loc;
            mod.Config.Env = JsEnvironment;
            mod.Config.Version = version;
            var urls = Shell.GetConfigAs<Dictionary<string, string>>("Services", false);
            if (urls != null)
            {
                mod.Config.Urls = new Dictionary<string, string>();
                foreach (var s in urls)
                {
                    mod.Config.Urls[s.Key] = WebUtils.FillConfigUrlParams(s.Value, Request);
                }
            }

            mod.Config.Domain = dom;
            mod.Config.BaseURL = dom == defaultTenant ? "/" : "/" + dom;
            mod.Config.Hash = isDevBuild ? "" : "~7d078181";
            mod.Chunks = files.Select(d => d.GetAfterLast("\\")).ToArray();

            return mod;
        }

        

    }
}
