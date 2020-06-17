using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Moldster
{
    public interface IServerConfig
    {
        string BaseURL { get; set; }
        string Domain { get; set; }
        string Locale { get; set; }
        string Env { get; set; }
        string Version { get; set; }
        string Hash { get; set; }
        Dictionary<string, string> Urls { get; set; }
    }
}
