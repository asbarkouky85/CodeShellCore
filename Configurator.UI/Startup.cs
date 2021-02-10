using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeShellCore.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Configurator.UI
{
    public class Startup : ShellStartup<UIShell>
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
