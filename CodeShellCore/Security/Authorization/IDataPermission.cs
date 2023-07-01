using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authorization
{
    public interface IDataPermission
    {
        bool CanInsert { get; set; }
        bool CanDelete { get; set; }
        bool CanUpdate { get; set; }
        bool CanViewDetails { get; set; }
    }
}
