using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Moldster
{
   public class DefaultServerConfig : IServerConfig
    {
        public string BaseURL { get; set; }
        public string Domain { get; set; }
        public string Locale { get; set; }
        public string Env { get; set; }
        public string Version { get; set; }
        public string Hash { get; set; }
        public Dictionary<string, string> Urls { get; set; }
    }
}
