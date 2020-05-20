using CodeShellCore.Data.Helpers;
using CodeShellCore.Tasks;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CodeShellCore.Http
{
    public class CodeShellHttpException : Exception
    {
        private string _message;
        public HttpStatusCode Status { get; private set; }
        public HttpResult HttpResult { get; private set; }
        public override string Message { get { return _message; } }
        public CodeShellHttpException(HttpResponseMessage mes, Uri uri = null, string method = null)
        {
            Status = mes.StatusCode;
            HttpResult = new HttpResult
            {
                RequestUrl = uri != null ? uri.AbsoluteUri : mes.RequestMessage?.RequestUri.ToString(),
                Code = (int)mes.StatusCode,
                Message = mes.StatusCode.ToString(),
                ExceptionMessage = mes.Content.ReadAsStringAsync().GetTaskResult(),
                Method = method ?? mes.RequestMessage?.Method.ToString()
            };
        }

        public CodeShellHttpException(HttpStatusCode code, string message)
        {
            Status = code;
            _message = message;
            HttpResult = new HttpResult
            {
                Code = (int)code,
                Message = code.ToString(),
                ExceptionMessage = message,
            };
        }

        public CodeShellHttpException(HttpResult message)
        {
            Status = (HttpStatusCode)message.Code;
            _message = message.Message;
            HttpResult = message;
        }
    }
}
