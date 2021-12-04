using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Helpers
{
    public class SubmitResult<T> : SubmitResult where T : class
    {
        public T Result { get; set; }
    }
}
