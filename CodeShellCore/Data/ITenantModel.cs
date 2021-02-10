using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public interface ITenantModel<TPrime> : IModel<TPrime>
    {
        TPrime TenantId { get; set; }
    }
}
