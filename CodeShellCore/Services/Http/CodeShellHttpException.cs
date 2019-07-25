using CodeShellCore.Data.Helpers;
using CodeShellCore.Tasks;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CodeShellCore.Services.Http
{
    public class CodeShellHttpException : Exception
    {
        private string _message;
        public HttpStatusCode Status { get; private set; }
        public HttpResult HttpResult { get; private set; }
        public override string Message { get { return _message; } }
        public CodeShellHttpException(HttpResponseMessage mes)
        {
            Status = mes.StatusCode;
            _message = mes.Content.ReadAsStringAsync().GetTaskResult();
        }

        public CodeShellHttpException(HttpStatusCode code, string message)
        {
            Status = code;
            _message = message;
        }

        public CodeShellHttpException(HttpResult message)
        {
            Status = (HttpStatusCode)message.Code;
            _message = message.Message;
            HttpResult = message;
        }
    }
}
