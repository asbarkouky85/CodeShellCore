using CodeShellCore.Cli.Requests;
using CodeShellCore.Cli.Routing;
using CodeShellCore.Cli.Services;
using CodeShellCore.Moldster;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Cli
{
    public class CliShell : CliDispatchShell
    {
        
        public static string ConfigurationApiPath { get; set; }

        protected override string sharedPathRoot => "./";

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
