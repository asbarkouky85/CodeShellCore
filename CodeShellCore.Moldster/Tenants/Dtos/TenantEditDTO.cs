﻿using CodeShellCore.Data.Helpers;
using CodeShellCore.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Tenants.Dtos
{
    public class TenantEditDTO : EditingDTO<Tenant>
    {
        public string Environment { get; set; }
        public string DbName { get; set; }
        public bool Force { get; set; }
    }
}