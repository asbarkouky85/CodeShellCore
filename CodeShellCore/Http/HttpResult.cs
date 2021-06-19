using CodeShellCore.Helpers;
using System.Net;

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
