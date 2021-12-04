using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Moldster.Dtos
{
    public class ServerConfigDto
    {
        [JsonProperty(PropertyName ="Domain")]
        public string Domain { get; set; }
        public string ApiUrl { get; set; }
        public bool Production { get; set; }
        public string DefaultLocale { get; set; }
        public string Version { get; set; }
        public Dictionary<string, string> Urls { get; set; }
    }
}
