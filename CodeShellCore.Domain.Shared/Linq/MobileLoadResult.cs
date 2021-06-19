using System;
using CodeShellCore.Helpers;

namespace CodeShellCore.Linq
{
    public class MobileLoadResult<T> : LoadResult<T>, IResult
        where T : class
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }

        public bool IsSuccess => Code == 0;
        private Exception exception;
        public MobileLoadResult(LoadResult<T> res)
        {
            Code = 0;
            Message = "Success";
            List = res.List;
            TotalCount = res.TotalCount;
        }

        public void SetException(Exception e, bool recurse = false)
        {
            exception = e;
            ExceptionMessage = e.GetMessageRecursive();
        }

        public Exception GetException()
        {
            return exception;
        }
    }
}
