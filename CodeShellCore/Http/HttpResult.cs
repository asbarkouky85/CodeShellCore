using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace CodeShellCore.Http
{
    public class HttpResult : Result
    {
        public string RequestUrl { get; set; }
        public string Method { get; set; }

        public HttpResult()
        {
            Code = 200;
        }

        public HttpResult(HttpStatusCode code, string message = null)
        {
            Code = (int)code;
            Message = message;
        }

        public void SetStatusCode(HttpStatusCode code)
        {
            Code = (int)code;
        }
        
    }
}
