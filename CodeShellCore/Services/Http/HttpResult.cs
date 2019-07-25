using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace CodeShellCore.Services.Http
{
    public class HttpResult : Result
    {
        public string RequestUrl { get; set; }
        public string Method { get; set; }

        public HttpResult InnerResult { get; set; }

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

        public override void SetException(Exception e, bool recurse = false)
        {
            _exception = e;
            if (Code == 200)
                Code = 500;

            if (_exception is CodeShellHttpException)
            {
                var res = (_exception as CodeShellHttpException).HttpResult;
                if ((_exception as CodeShellHttpException).HttpResult != null)
                {
                    Code = res.Code;
                    Message = res.Message;
                    ExceptionMessage = res.ExceptionMessage;
                    StackTrace = res.StackTrace;
                    return;
                }
            }

            if (_exception is CodeShellHttpException)
            {
                var res = (_exception as CodeShellHttpException).HttpResult;
                if ((_exception as CodeShellHttpException).HttpResult != null)
                {
                    Code = res.Code;
                    Message = res.Message;
                    ExceptionMessage = res.ExceptionMessage;
                    StackTrace = res.StackTrace;
                    return;
                }
            }

            if (_exception.InnerException != null && _exception.InnerException is CodeShellHttpException)
            {
                try
                {
                    InnerResult = _exception.InnerException.Message.FromJson<HttpResult>();

                    ExceptionMessage = "Check inner result";
                }
                catch
                {
                    ExceptionMessage = e.GetMessageRecursive();
                }
            }
            else
            {
                ExceptionMessage = e.GetMessageRecursive();
            }


            if (e.StackTrace != null)
                StackTrace = e.GetStackTrace();
        }

    }
}
