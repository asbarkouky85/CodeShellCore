using CodeShellCore.Cli.Requests.Handlers;
using CodeShellCore.Cli.Routing;
using CodeShellCore.Cli.Services;
using CodeShellCore.Moldster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace CodeShellCore.Cli
{
    public class CliShell : CliDispatchShell
    {
        
        public static string ConfigurationApiPath { get; set; }
        public CliShell(string[] args) : base(args)
        {
        }

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);

            coll.AddMoldsterDbData();
            coll.AddMoldsterCli();

            coll.AddTransient<IPathsService, CliPathsService>();
        }

        protected override void RegisterHandlers(ICliDispatcherBuilder builder)
        {
            builder.AddHandler<MigrateOldAppHandler>("migrate");
        }

        
    }
}
