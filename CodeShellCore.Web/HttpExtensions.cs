using CodeShellCore;
using CodeShellCore.Json;
using CodeShellCore.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.AspNetCore.Mvc
{
    public static class HttpExtensions
    {
        public static string GetLocaleFromCookie(this HttpRequest req)
        {
            string loc = Shell.DefaultCulture.TwoLetterISOLanguageName;
            if (req.Cookies.ContainsKey("Locale"))
                loc = req.Cookies["Locale"];

            return loc;
        }

        public static IWebHostBuilder UseKestrelHttps(this IWebHostBuilder builder)
        {
            var lSettings = "Properties/launchSettings.json";
            int httpsPort = 5001;
            int httpPort = 5000;

            if (File.Exists(lSettings))
            {
                var data = File.ReadAllText(lSettings);
                var obj = (JObject)JsonConvert.DeserializeObject(data);
                var httpUrl = obj.GetPathAsString("iisSettings:iisExpress:applicationUrl");
                var httpPortString = httpUrl?.GetAfterLast(":")?.Replace("/", "");
                var httpsPortString = obj.GetPathAsString("iisSettings:iisExpress:sslPort");
                if (!string.IsNullOrEmpty(httpPortString))
                    int.TryParse(httpPortString, out httpPort);
                if (!string.IsNullOrEmpty(httpsPortString))
                    int.TryParse(httpsPortString, out httpsPort);
                httpsPort = httpsPort == 0 ? 5001 : httpsPort;
            }

            builder.ConfigureKestrel(op =>
             {
                 op.Listen(IPAddress.Any, httpsPort, lop =>
                 {
                     lop.Protocols = HttpProtocols.Http1AndHttp2;
                     lop.UseHttps(StoreName.Root, "localhost");
                 });
                 if (httpPort != httpsPort)
                 {
                     op.Listen(IPAddress.Any, httpPort);
                 }
             });
            return builder;
        }

        public static bool IsProccessed(this HttpContext con)
        {
            return con.Items.ContainsKey("IsProccessed");
        }

        public static string GetDeviceIdFromCookie(this HttpRequest req)
        {
            string did = null;
            if (req.Cookies.ContainsKey("DID"))
                did = req.Cookies["DID"];

            return did;
        }

        public static void SetLocaleCookie(this HttpResponse req, string loc)
        {
            req.Cookies.Append("Locale", loc);
        }

        public static bool PathIsEmpty(this HttpRequest req)
        {
            return !req.Path.HasValue || req.Path.Value.Length == 0 || req.Path.Value == "/";
        }


    }
}
