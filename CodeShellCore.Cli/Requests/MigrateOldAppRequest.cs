﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Cli.Requests
{
    public class MigrateOldAppRequest
    {
        public string UIPath { get; set; }
        public string TenantCode { get; set; }
        public string ConfigurationApiPath { get; set; }
        public string Environment { get; set; }
    }
}