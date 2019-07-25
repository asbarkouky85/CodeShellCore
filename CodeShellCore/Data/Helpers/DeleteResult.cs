using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Helpers
{
    public class DeleteResult : SubmitResult
    {
        public bool CanDelete { get; set; }
        public string tableName { get; set; }
    }
}
