using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Filters;


using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Text.Localization;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Text;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Services.Http;
using Newtonsoft.Json;
using CodeShellCore.Files;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.Security;

namespace CodeShellCore.Reporting
{
    public static class ReportingExtensions
    {
        public static FileContentResult ToFileResult(this FileBytes res)
        {
            var x = new FileContentResult(res.Bytes, res.MimeType);
            if (res.FileName != null)
                x.FileDownloadName = res.FileName + res.Extension;
            return x;
        }
    }
}

namespace CodeShellCore
{
    public static class CoreExtensions
    {
        public static string GetCurrentToken(this IAuthorizationService service)
        {
            if (service.SessionManager is TokenSessionManager)
            {
                return ((TokenSessionManager)service.SessionManager).GetTokenFromHeader();
            }
            return null;
        }
    }
}

namespace CodeShellCore.Web
{
    public static class Extenstions
    {
        public static HttpResponseMessage ToWebResponse(this SubmitResult data)
        {
            HttpResponseMessage mes = new HttpResponseMessage();
            if (data.Code == 0)
                mes.StatusCode = HttpStatusCode.OK;
            else
                mes.StatusCode = HttpStatusCode.ExpectationFailed;

            mes.Content = new StringContent(data.ToJson());
            return mes;
        }

        public static HttpResponseMessage ToWebResponse(this LoginResult data)
        {
            HttpResponseMessage mes = new HttpResponseMessage();
            if (data.Success)
                mes.StatusCode = HttpStatusCode.OK;
            else
                mes.StatusCode = HttpStatusCode.ExpectationFailed;

            mes.Content = new StringContent(data.ToJson());
            return mes;
        }

        public static string GetDeviceIdIfWeb(this ISessionManager sess)
        {
            if (sess is WebSessionManagerBase)
            {
                return (sess as WebSessionManagerBase).GetDeviceId();
            }
            return null;
        }

        public static object CheckRefreshTokenWEB(this ISessionManager sess,string refreshToken)
        {
            if (sess is TokenSessionManager)
            {
                return (sess as TokenSessionManager).CheckRefreshToken(refreshToken);
            }
            return null;
        }

        public static string GetHostUrl(this HttpRequest req)
        {
            return (req.IsHttps ? "https://" : "http://") + req.Host;
        }

        

        public static string GetHeader(this HttpContext context, string key)
        {
            if (context.Request.Headers.Keys.Contains(key))
                return context.Request.Headers[key];

            return null;
        }

        public static string GetFullUrl(this HttpRequest req)
        {
            return req.GetHostUrl() + req.Path + req.QueryString.Value;

        }
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }

        public static string GetRemoteAddress(this HttpContext con)
        {
            return con.Connection.RemoteIpAddress + ":" + con.Connection.RemotePort;
        }

        public static IActionResult Respond(this ActionContext context, LoginResult res)
        {
            if (!res.Success)
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;

            return new JsonResult(res);
        }

        public static IActionResult Respond(this ActionContext context, object ob, HttpStatusCode? code = null)
        {
            if (ob == null)
            {
                code = HttpStatusCode.NotFound;
                ob = new { };
            }

            context.HttpContext.Response.StatusCode = (int)(code ?? HttpStatusCode.OK);

            return new JsonResult(ob);
        }

        public static IActionResult Respond(this ActionContext context, HttpResult res)
        {
            context.HttpContext.Response.StatusCode = res.Code;
            return new JsonResult(res);
        }

        public static string ReadBodyAsString(this HttpRequest req)
        {
            string st;
            using (StreamReader reader = new StreamReader(req.Body))
            {
                st = reader.ReadToEnd();
            }
            return st;
        }

        public static string GetController(this HttpContext http)
        {
            return (string)http.GetRouteData().Values["controller"];
        }

        public static string GetAction(this HttpContext http)
        {
            return (string)http.GetRouteData().Values["action"];
        }

        public static void LoadCultureFromHeader(this HttpContext http)
        {

            if (http.Request.Headers.Keys.Contains("locale"))
            {
                string loc = http.Request.Headers["locale"];

                Language lan = http.RequestServices.GetService<Language>();
                if (loc.Length == 2)
                    lan.SetCulture(loc);
            }
        }


    }
}
