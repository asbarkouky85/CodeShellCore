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
    public class LegacySpaModelBuilder : ILegacySpaModelBuilder
    {

        private string DefaultTenant;
        protected HttpRequest Request { get; set; }
        protected virtual IServerConfig ServerConfig { get { return new DefaultServerConfig(); } }
        protected virtual string[] Domains => Tenants.Select(d => d.Key).ToArray();
        protected virtual Func<IndexModel, IActionResult> EmptyUrlHandler { get; }

        protected virtual Dictionary<string, TenantInfoItem> Tenants
        {
            get
            {
                var _tenants = new Dictionary<string, TenantInfoItem>();
                string infoFile = Path.Combine(Shell.AppRootPath, "tenantInfo.json");
                if (File.Exists(infoFile))
                {
                    _tenants = System.IO.File.ReadAllText(infoFile).FromJson<Dictionary<string, TenantInfoItem>>();
                }
                return _tenants;
            }
        }

        protected string JsEnvironment
        {
            get
            {
                string env = Shell.GetConfigAs<string>("env", false);
                return (env == null || env == "production") ? "prod" : "dev";
            }
        }

        protected virtual string RequestedDomain
        {
            get
            {
                string[] st = Request.Path.Value.Split('/');
                foreach (var p in st)
                {
                    if (Domains.Contains(p))
                        return p;
                }
                return DefaultTenant;
            }
        }


        public virtual IndexModel BuildModel(HttpRequest request, string defaultTenant, string defaultTitle)
        {
            Request = request;
            DefaultTenant = defaultTenant;

            string requestedDomain = RequestedDomain;
            string useDomain = requestedDomain;
            bool isDevBuild = JsEnvironment == "dev";

            TenantInfoItem useTenantInfo = null;

            if (Tenants.TryGetValue(requestedDomain, out TenantInfoItem tenantInfo))
            {

                if (!string.IsNullOrEmpty(tenantInfo.UseTenantPackage))
                {
                    useDomain = tenantInfo.UseTenantPackage;
                    Tenants.TryGetValue(tenantInfo.UseTenantPackage, out useTenantInfo);
                }
            }


            string version = Shell.ProjectAssembly.GetVersionString();
            if (useTenantInfo != null)
            {
                version = useTenantInfo.Version;
            }
            else if (tenantInfo?.Version != null)
            {
                version = tenantInfo.Version;
            }

            string loc = Request.GetLocaleFromCookie();
            string package = isDevBuild ? "dev" : "v" + version;
            string path = Path.Combine(Shell.AppRootPath, Shell.PublicRoot, "dist", package);
            string search = isDevBuild ? "dev*.js" : useDomain + "-" + package + "*.js";
            Utils.CreateFolderForFile(path + "\\n.x");
            string[] files = Directory.GetFiles(path, search);

            var mod = new IndexModel
            {
                Title = defaultTitle,
                Config = ServerConfig,
                PackageDomain = useDomain,
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

            mod.Config.Domain = requestedDomain;
            mod.Config.BaseURL = requestedDomain == defaultTenant ? "/" : "/" + requestedDomain;
            mod.Config.Hash = isDevBuild ? "" : "~7d078181";
            mod.Chunks = files.Select(d => d.GetAfterLast("\\")).ToArray();

            return mod;
        }



    }
}
