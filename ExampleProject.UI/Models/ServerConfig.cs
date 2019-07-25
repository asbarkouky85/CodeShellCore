using CodeShellCore.Web.Moldster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleProject.UI.Models
{
    public class ServerConfig : IServerConfig
    {
        public string BaseURL { get; set; }
        public string Domain { get; set; }
        public string Locale { get; set; }
        public string Env { get; set; }
        public string Version { get; set; }
        public string Hash { get; set; }
    }
}
