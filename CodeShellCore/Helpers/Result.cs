using CodeShellCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Helpers
{
    public class Result : IResult
    {
        public Dictionary<string, object> Data { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string[] StackTrace { get; set; }
        public virtual bool IsSuccess { get { return Code == 0; } }
        public Result InnerResult { get; set; }

        protected Exception _exception;

        public Result()
        {
            Data = new Dictionary<string, object>();
        }

        public Result(int code)
        {
            Code = code;
            Data = new Dictionary<string, object>();
        }

        public TOut MapToResult<TOut>() where TOut : Result
        {
            var inst = Activator.CreateInstance<TOut>();

            inst.Message = Message;
            inst.StackTrace = StackTrace;
            inst.Data = Data;
            var ex = inst.GetException();
            if (ex != null)
                inst.SetException(ex);
            else
                inst.ExceptionMessage = ExceptionMessage;
            inst.Code = Code;

            return inst;
        }

        public Exception GetException()
        {
            return _exception;
        }

        public static MobileResult<T> MakeMobileResult<T>(T data)
        {
            var res = new MobileResult<T>();
            res.Code = 0;
            res.Message = "Success";
            res.Data = data;
            return res;
        }

        public virtual void SetException(Exception e, bool recurse = false)
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
            
            if (_exception.InnerException != null && _exception.InnerException is CodeShellHttpException)
            {
                try
                {
                    InnerResult = ((CodeShellHttpException)_exception.InnerException).HttpResult;
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

        public T ReadFromDataAs<T>(string index) where T : class
        {
            if (Data.TryGetValue(index, out object item))
            {
                JObject ob = (JObject)item;
                return ob.ToObject<T>();
            }
            return null;
        }
    }


}
