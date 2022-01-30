using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public interface IChangeColumns
    {
        long? CreatedBy { get; set; }
        long? UpdatedBy { get; set; }
        DateTime? CreatedOn { get; set; }
        DateTime? UpdatedOn { get; set; }
    }
}
