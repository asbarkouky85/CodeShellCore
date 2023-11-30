using CodeShellCore.Data.Helpers;
using CodeShellCore.Files;
using CodeShellCore.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Tenants
{
    [EntityName("Tenant")]
    public class TenantEditDTO : EditingDTO<TenantDto>
    {
        public string Environment { get; set; }
        public string DbName { get; set; }
        public bool Force { get; set; }
    }
}
