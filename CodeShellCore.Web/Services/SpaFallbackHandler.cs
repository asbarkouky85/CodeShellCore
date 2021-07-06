using CodeShellCore.Helpers;
using CodeShellCore.Web.Moldster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Services
{
    public class SpaFallbackHandler : ISpaFallbackHandler
    {
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
        protected virtual TenantInfoItem[] Tenants { get; }
        protected virtual string DefaultTenant { get; }

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
                    if (req.Path.Value.StartsWith("/"+t.Code.ToLower()))
                    {
                        return "wwwroot/" + t.Code.ToLower() + "/index.html";
                    }
                }
            }
            if(!string.IsNullOrEmpty(DefaultTenant))
                return "wwwroot/" + DefaultTenant.ToLower() + "/index.html";
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
            await con.Response.WriteAsync(file);
        }
    }
}
