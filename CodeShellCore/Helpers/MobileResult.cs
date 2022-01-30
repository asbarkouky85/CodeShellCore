using System;

namespace CodeShellCore.Helpers
{
    public class MobileResult<T> : IResult
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        private Exception _ex;
        public string ExceptionMessage { get; private set; }

        public bool IsSuccess => Code == 0;

        public Exception GetException()
        {
            return _ex;
        }

        public void SetException(Exception e, bool recurse = false)
        {
            Code = 1;
            ExceptionMessage = e.GetMessageRecursive();
            _ex = e;
        }
    }
}
