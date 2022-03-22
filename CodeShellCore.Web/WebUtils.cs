using CodeShellCore.Types;
using Microsoft.AspNetCore.Http;

namespace CodeShellCore.Web
{
    public class WebUtils
    {
        public static string FillConfigUrlParams(string formattedUrl, string currentHost = "localhost", bool https = false)
        {
            string subject = formattedUrl;
            if (subject != null)
            {
                subject = subject.Replace("{CURRENT_HOST}", currentHost);
                subject = subject.Replace("{CURRENT_PROTOCOL}", https ? "https" : "http");
            }
            return subject;
        }

        public static string FillConfigUrlParams(string formattedUrl, HttpRequest Request)
        {
            return FillConfigUrlParams(formattedUrl, Request.Host.Host, Request.IsHttps);
        }

        public static string GetAssemblyInfoHtml(string title = null)
        {
            string serviceName = Shell.ProjectAssembly.GetName().Name;
            string version = Shell.ProjectAssembly.GetVersionString();

            return "<head><title>" + (title ?? serviceName) + "</title></head><h1>" + serviceName + "</h1><h2>Version : " + version + "</h2>";
        }

    }
}
