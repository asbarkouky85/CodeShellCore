using CodeShellCore.CliDispatch;
using CodeShellCore.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.ToolSet
{
    [DependsOn(
        typeof(CodeShellCliDispatchModule)
        )]
    public class ToolsApplicationModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddTransient<IModuleClassGenerationService, ModuleClassGenerationService>();
        }
    }
}
