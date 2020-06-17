using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Helpers
{
    public class BaseDTO<TPrime> : IEditable
    {
        public TPrime Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public string State { get; set; }
    }
}
