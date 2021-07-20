using CodeShellCore.Web.Moldster;
using CodeShellCore.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.UI.Handlers
{
    public class ExampleFallBackHandler : SpaFallbackHandler
    {
        protected override TenantInfoItem[] Tenants => new[] { new TenantInfoItem { Code = "tenant_1" }, new TenantInfoItem { Code = "tenant_2" } };
        protected override string DefaultTenant => "tenant_1";
    }
}
