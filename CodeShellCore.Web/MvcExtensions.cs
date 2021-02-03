using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Text.Localization;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Text;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Http;
using Newtonsoft.Json;
using CodeShellCore.Files;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.Security;
using System;
using CodeShellCore.Files.Logging;
using CodeShellCore.Helpers;
using Microsoft.Extensions.Primitives;
using System.ComponentModel;
using System.Linq;
using CodeShellCore.Web;

namespace Microsoft.AspNetCore.Mvc
{
    public static class MvcExtensions
    {

        public static FileContentResult ToFileResult(this FileBytes res)
        {
            var x = new FileContentResult(res.Bytes, res.MimeType);
            if (res.FileName != null)
                x.FileDownloadName = res.FileName + res.Extension;
            return x;
        }

        public static HttpResult HandleRequestError(this HttpContext context, Exception excep)
        {
            HttpRequest req = context.Request;
            string url = req.GetFullUrl();
            HttpResult res = new HttpResult
            {
                Code = 1,
                RequestUrl = url,
                Method = req.Method
            };

            res.SetException(excep);

            if (excep is UnauthorizedAccessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                res.Message = "unauthorized_operation";
                res.StackTrace = new string[0];
                foreach (var d in excep.Data.Keys)
                    res.Data[d.ToString()] = excep.Data[d];
            }

            else if (excep is CodeShellHttpException)
                context.Response.StatusCode = (int)((CodeShellHttpException)excep).Status;
            else if (excep is ArgumentOutOfRangeException)
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            else if (excep is ArgumentException)
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            else
                context.Response.StatusCode = 500;

            Logger.WriteException(excep);
            return res;
        }

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
            if (data.IsSuccess)
                mes.StatusCode = HttpStatusCode.OK;
            else
                mes.StatusCode = HttpStatusCode.ExpectationFailed;

            mes.Content = new StringContent(data.ToJson());
            return mes;
        }



        public static string CheckRefreshTokenWEB(this ISessionManager sess, string refreshToken)
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
            if (!res.IsSuccess)
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

        public static void SetProccessed(this HttpContext con)
        {
            con.Items["IsProccessed"] = true;
        }

        public static void ProcessOnce(this HttpContext http, string token = null)
        {
            if (!http.IsProccessed())
            {
                http.LoadCultureFromHeader();
                http.ReadClientData();

                var _manager = http.RequestServices.GetService<ISessionManager>();
                _manager?.AuthorizationRequest();
                http.SetProccessed();
            }

            if (token != null)
            {
                var _manager = http.RequestServices.GetService<ISessionManager>();
                _manager?.AuthorizationRequest(token);
            }
        }

        public static void LoadCultureFromHeader(this HttpContext http)
        {
            if (http.Request.Headers.Keys.Contains(HttpHeaderKeys.Language))
            {
                string loc = http.Request.Headers[HttpHeaderKeys.Language];

                Language lan = http.RequestServices.GetService<Language>();
                if (loc.Length == 2)
                    lan.SetCulture(loc);
            }
        }

        public static void ReadClientData(this HttpContext http)
        {
            var cl = http.RequestServices.GetService<ClientData>();
            if (http.Request.Headers.Keys.Contains(HttpHeaderKeys.IsMobile))
                cl.IsMobile = true;

            if (http.Request.Headers.Keys.Contains(HttpHeaderKeys.ClientId))
                cl.ClientId = http.Request.Headers[HttpHeaderKeys.ClientId];

            var deviceId = http.Request.GetDeviceIdFromCookie();

            if (!string.IsNullOrEmpty(deviceId))
            {
                cl.DeviceId = deviceId;
            }
            else if (http.Request.Headers.Keys.Contains(HttpHeaderKeys.MobileDeviceId))
            {
                deviceId = http.Request.Headers[HttpHeaderKeys.MobileDeviceId];
                var lan = http.RequestServices.GetService<ClientData>();
                lan.DeviceId = deviceId;
            }
        }

        public static T ReadAs<T>(this IQueryCollection query)
        {
            var ps = typeof(T).GetProperties();
            var res = Activator.CreateInstance<T>();
            foreach (var p in ps)
            {
                var k = p.Name.LCFirst();
                if (query.TryGetValue(k, out StringValues v))
                {
                    var value = v.First();
                    var foo = TypeDescriptor.GetConverter(p.PropertyType);
                    var valueConv = foo.ConvertFromInvariantString(value);
                    p.SetValue(res, valueConv);
                }
            }
            return res;
        }
    }
}
