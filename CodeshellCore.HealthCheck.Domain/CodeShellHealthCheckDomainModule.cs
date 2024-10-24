using CodeShellCore.Modularity;
using CodeShellCore;
using CodeShellCore.Extensions.DependencyInjection;

namespace CodeShellCore.HealthCheck
{
    [DependsOn(
        typeof(CodeShellDomainModule)
        )]
    public class CodeShellHealthCheckDomainModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddDataSeeders<CodeShellHealthCheckDomainModule>();
        }
    }
}