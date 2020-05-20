using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Definitions
{
    public class DbCreationRequest
    {
        public bool? Force { get; set; }
        public string Environment { get; set; }
        public string TenantCode { get; set; }
        public string DbName { get; set; }
        public long? Id { get; set; }
    }
}
