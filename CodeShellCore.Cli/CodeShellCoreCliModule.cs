using CodeShellCore.Cli.Requests;
using CodeShellCore.CliDispatch;
using CodeShellCore.Modularity;
using CodeShellCore.Moldster;

namespace CodeShellCore.Cli
{
    [DependsOn(
        typeof(MoldsterApplicationModule),
        typeof(MoldsterEntityFrameworkModule),
        typeof(CodeShellCliDispatchModule)
        )]
    public class CodeShellCoreCliModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            var builder = context.Services.GetCliRouteBuilder();
            builder.AddHandler<MigrateOldAppHandler>("migrate");
            builder.AddHandler<RestructureAppHandler>("restruct");
        }
    }
}
