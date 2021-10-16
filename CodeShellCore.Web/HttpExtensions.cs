using CodeShellCore;
using Microsoft.AspNetCore.Http;

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
