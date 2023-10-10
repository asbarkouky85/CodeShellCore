using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public class SubmissionFailedException : Exception
    {
        private string _message;
        public override string Message => _message;
        public Result Result { get; private set; }
        public SubmissionFailedException(Result res)
        {
            _message = res.Message;
            Result = res;
        }
    }
}
