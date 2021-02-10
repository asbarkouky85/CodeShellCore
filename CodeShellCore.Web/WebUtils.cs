using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

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

        public static string FillConfigUrlParams(string formattedUrl,HttpRequest Request)
        {
            return FillConfigUrlParams(formattedUrl,Request.Host.Host,Request.IsHttps);
        }
    }
}
