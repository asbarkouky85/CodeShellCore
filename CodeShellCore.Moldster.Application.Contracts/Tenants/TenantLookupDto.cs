using CodeShellCore.Data.Lookups;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Tenants
{
    public class TenantLookupDto : Named<object>
    {
        public string Code { get; set; }
    }
}
