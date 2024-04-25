using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Configuration
{
    public class ServerConfigDto
    {
        public string TenantCode { get; set; }
        public long TenantId { get; set; }
        public string Version { get; set; }
        public Dictionary<string, string> Urls { get; set; }
        public Dictionary<string, string> Settings { get; set; }
    }
}
