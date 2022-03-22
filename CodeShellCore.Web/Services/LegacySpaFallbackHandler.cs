using CodeShellCore.Text;
using CodeShellCore.Types;
using CodeShellCore.Web.Moldster;
using CodeShellCore.Web.Razor.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Services
{
    public class LegacySpaFallbackHandler : ISpaFallbackHandler
    {
        protected IRazorRenderingService Razor { get; private set; }
        protected HttpContext HttpContext { get; set; }
        protected virtual Func<HttpContext, Task> EmptyUrlHandler { get; }
        public virtual string DefaultTenant => Shell.GetConfigAs<string>("DefaultTenant", false);

        public LegacySpaFallbackHandler(IRazorRenderingService razor)
        {
            Razor = razor;
        }

        protected virtual Dictionary<string, TenantInfoItem> Tenants
        {
            get
            {
                var _tenants = new Dictionary<string, TenantInfoItem>();
                string infoFile = Path.Combine(Shell.AppRootPath, "tenantInfo.json");
                if (File.Exists(infoFile))
                {
                    _tenants = File.ReadAllText(infoFile).FromJson<Dictionary<string, TenantInfoItem>>();
                }
                return _tenants;
            }
        }

        protected virtual IndexModel InitializeModel(string packageDomain, string packageId, string defaultTitle = null)
        {
            return new IndexModel
            {
                Title = defaultTitle,
                PackageDomain = packageDomain,
                PackageId = packageId
            };
        }

        protected virtual IServerConfig InitializeServerConfig()
        {
            return new DefaultServerConfig();
        }

        protected virtual string GetJsEnvironment()
        {
            string env = Shell.GetConfigAs<string>("env", false);
            return (env == null || env == "production") ? "prod" : "dev";
        }

        protected virtual string GetTenantCodeFromUrl()
        {
            string[] st = HttpContext.Request.Path.Value.Split('/');
            foreach (var p in st)
            {
                if (Tenants.ContainsKey(p))
                    return p;
            }
            return null;
        }

        protected virtual Dictionary<string, string> GetUrlDictionary()
        {
            var urls = Shell.GetConfigAs<Dictionary<string, string>>("Services", false);
            if (urls != null)
            {
                foreach (var s in urls)
                {
                    urls[s.Key] = WebUtils.FillConfigUrlParams(s.Value, HttpContext.Request);
                }
            }
            return urls;
        }

        protected virtual PackageDefinition GetPackage(string tenant)
        {
            TenantInfoItem useTenantInfo = null;
            var package = new PackageDefinition();
            package.Domain = tenant;
            package.Version = Shell.ProjectAssembly.GetVersionString();

            if (Tenants.TryGetValue(tenant, out TenantInfoItem tenantInfo))
            {

                if (!string.IsNullOrEmpty(tenantInfo.UseTenantPackage))
                {
                    package.Domain = tenantInfo.UseTenantPackage;
                    Tenants.TryGetValue(tenantInfo.UseTenantPackage, out useTenantInfo);
                }
            }

            if (useTenantInfo != null)
            {
                package.Version = useTenantInfo.Version;
            }
            else if (tenantInfo?.Version != null)
            {
                package.Version = tenantInfo.Version;
            }
            return package;
        }


        public IndexModel BuildModel(string tenant, string defaultTitle)
        {
            string jsEnvironment = GetJsEnvironment();
            var packageDefinition = GetPackage(tenant);

            bool isDevBuild = jsEnvironment == "dev";
            string packageId = isDevBuild ? "dev" : "v" + packageDefinition.Version;

            var mod = InitializeModel(packageDefinition.Domain, packageId, defaultTitle);

            mod.Config = InitializeServerConfig();

            mod.Config.Locale = HttpContext.Request.GetLocaleFromCookie();
            mod.Config.Env = jsEnvironment;
            mod.Config.Version = packageDefinition.Version;
            mod.Config.Urls = GetUrlDictionary();
            mod.Config.Domain = tenant;
            mod.Config.BaseURL = tenant == DefaultTenant ? "/" : "/" + tenant;

            return mod;
        }

        protected bool UrlIsEmpty()
        {
            return string.IsNullOrEmpty(HttpContext.Request.Path) || HttpContext.Request.Path == "" || HttpContext.Request.Path == "/";
        }

        protected virtual string GetTenantHtml(IndexModel model)
        {
            return Razor.RenderPartial(HttpContext, "Index", model);
        }

        protected virtual string GetDefaultHtml(string title = null)
        {
            return WebUtils.GetAssemblyInfoHtml(title);
        }

        public async Task HandleRequestAsync(HttpContext con, string defaultTitle = null)
        {

            HttpContext = con;

            if (UrlIsEmpty() && EmptyUrlHandler != null)
            {
                await EmptyUrlHandler(con);
            }
            else
            {
                con.Response.ContentType = "text/html";
                var tenantCode = GetTenantCodeFromUrl() ?? DefaultTenant;
                var html = "";
                if (tenantCode != null)
                {
                    var model = BuildModel(tenantCode, defaultTitle);
                    html = GetTenantHtml(model);
                }
                else
                {
                    html = GetDefaultHtml(defaultTitle);
                }
                await con.Response.WriteAsync(html);
            }
        }

        public Task HandleRequestAsync(HttpContext con, Func<Task> next)
        {
            if (con.Request.Path.HasValue && (con.Request.Path.Value.Contains("__webpack_hmr") || con.Request.Path.Value.Contains("hot-update.js")))
            {
                return next.Invoke();
            }
            return HandleRequestAsync(con);
        }
    }
}
