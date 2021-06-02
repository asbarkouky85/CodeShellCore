using CodeShellCore.Cli.Requests.Handlers;
using CodeShellCore.Cli.Routing;
using CodeShellCore.Cli.Services;
using CodeShellCore.Moldster;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Cli
{
    public class CliShell : CliDispatchShell
    {
        public CliShell(string[] args) : base(args)
        {
        }

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);

            coll.AddMoldsterDbData(Configuration);
            coll.AddMoldsterCli();

            coll.AddTransient<IPathsService, CliPathsService>();
        }

        protected override void RegisterHandlers(ICliDispatcherBuilder builder)
        {
            builder.AddMoldsterDispatchers();
            builder.AddHandler<MigrateOldAppHandler>("migrate");
        }
    }
}
