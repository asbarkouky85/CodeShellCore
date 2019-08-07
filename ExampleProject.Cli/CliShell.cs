using CodeShellCore.Caching;
using CodeShellCore.Cli;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleProject.Cli
{
    public class CliShell : ConsoleShell
    {
        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddRedisCaching();
        }
    }
}
