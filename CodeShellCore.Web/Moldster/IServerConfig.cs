using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Moldster
{
    public interface IServerConfig
    {
        string Domain { get; set; }
        string Version { get; set; }
    }
}
