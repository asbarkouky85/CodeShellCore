using CodeShellCore.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Dto
{
    public class TenantEditDTO : EditingDTO<Tenant>
    {
        public string Environment { get; set; }
        public string DbName { get; set; }
        public bool Force { get; set; }
    }
}
