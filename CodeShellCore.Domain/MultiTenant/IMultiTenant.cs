using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MultiTenant
{
    public interface IMultiTenant
    {
        long? TenantId { get; }
    }
}
