using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Helpers
{
    public class Result
    {
        public Dictionary<string, object> Data { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string[] StackTrace { get; set; }
        public virtual bool IsSuccess { get { return Code == 0; } }

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

        public virtual void SetException(Exception e, bool recurse = false)
        {
            _exception = e;
            ExceptionMessage = e.GetMessageRecursive(true);
            StackTrace = e.GetStackTrace(recurse, true);
            if (Code == 0)
                Code = 1;
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
