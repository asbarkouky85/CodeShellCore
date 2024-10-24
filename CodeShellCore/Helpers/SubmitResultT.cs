using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Helpers
{
    public class EntitySubmitResult<T> : SubmitResult where T : class
    {
        public T Result { get; set; }
    }
}
