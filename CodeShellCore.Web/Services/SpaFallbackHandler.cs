using CodeShellCore.Helpers;
using CodeShellCore.Web.Moldster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Text;

namespace CodeShellCore.Web.Services
{
    public class SpaFallbackHandler : ISpaFallbackHandler
    {
        public virtual string DefaultTenant => Shell.GetConfigAs<string>("DefaultTenant", false);
        protected string CurrentTenant { get; set; }
        private static string _defaultHtml = @"<!DOCTYPE html>
<html>

<head>
    <base href=""/"">
</head>
<body>
    <h1>Welcome</h2>
</body>
</html>
";
        protected virtual TenantInfoItem[] Tenants
        {
            get
            {
                var tens = GetTenants();
                return tens.Select(e => e.Value).ToArray();
            }
        }
        

        protected virtual Dictionary<string, TenantInfoItem> GetTenants()
        {
            var _tenants = new Dictionary<string, TenantInfoItem>();
            string infoFile = Path.Combine(Shell.AppRootPath, "tenantInfo.json");
            if (File.Exists(infoFile))
            {
                _tenants = File.ReadAllText(infoFile).FromJson<Dictionary<string, TenantInfoItem>>();
            }
            return _tenants;
        }

        /// <summary>
        /// Default is 'wwwroot/index.html'
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        protected virtual string GetIndexFilePath(HttpRequest req)
        {
            if (Tenants != null)
            {
                foreach (var t in Tenants)
                {
                    if (req.Path.Value.StartsWith("/" + t.Code.Replace("-", "").ToLower()))
                    {
                        CurrentTenant = t.Code;
                        return "wwwroot/" + t.Code.ToLower() + "/index.html";
                    }
                }
            }

            if (!string.IsNullOrEmpty(DefaultTenant))
            {
                CurrentTenant = DefaultTenant;
                return "wwwroot/" + DefaultTenant.ToLower() + "/index.html";
            }

            return "wwwroot/index.html";
        }

        public virtual async Task HandleRequestAsync(HttpContext con)
        {
            var indexPath = GetIndexFilePath(con.Request);
            if (!File.Exists(indexPath))
            {
                Utils.CreateFolderForFile(indexPath);
                File.WriteAllText(indexPath, _defaultHtml);
            }
            var file = File.ReadAllText(indexPath);
            con.Response.ContentType = "text/html";
            if (CurrentTenant != null)
            {
                file = file.Replace("<base href=\"/\">", "<base href=\"/" + CurrentTenant.ToLower() + "/\">");
                con.Response.Cookies.Append("current_tenant", CurrentTenant, new CookieOptions { Expires = DateTime.Now.AddYears(1), Path = "/" });
            }
            
            await con.Response.WriteAsync(file);


        }
    }
}
