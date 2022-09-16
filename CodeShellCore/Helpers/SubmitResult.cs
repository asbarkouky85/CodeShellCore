using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Data.Helpers
{
    public class SubmitResult : Result
    {

        public int AffectedRows { get; set; }


        public SubmitResult()
        {
            Code = 0;
            AffectedRows = 0;
            Message = "No Changes";
        }

        public SubmitResult(int code, string message = "No Changes")
        {
            Code = code;
            Message = message;
        }

        public SubmitResult<T> ToSubmitResult<T>() where T : class
        {
            var res = MapToResult<SubmitResult<T>>();
            res.AffectedRows = AffectedRows;
            return res;
        }

    }
}
