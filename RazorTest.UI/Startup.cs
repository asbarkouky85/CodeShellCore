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

namespace RazorTest.UI
{
    public class Startup : ShellStartup<RazorAppShell>
    {
        public Startup(IConfiguration config) : base(config)
        {
        }
    }
}
