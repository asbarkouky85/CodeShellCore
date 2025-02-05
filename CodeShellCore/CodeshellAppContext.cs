using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore
{
    public class CodeshellAppContext
    {
        public IServiceCollection Services { get; private set; }
        public IConfiguration Configuration { get; private set; }
        public string[] Arguments { get; private set; }

        public CodeshellAppContext(IServiceCollection coll, IConfiguration conf, string[] args = null)
        {
            Services = coll;
            Configuration = conf;
            Arguments = args ?? new string[0];
        }
    }
}
