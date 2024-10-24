using CodeShellCore.Cli;
using CodeShellCore.Data;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore
{
    [DependsOn(typeof(CodeShellDomainSharedModule))]
    public class CodeShellDomainModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddTransient<IOutputWriter, ConsoleOutputWriter>();
            context.Services.AddSingleton<ICollectionConfigService, CollectionConfigService>();
            context.Services.AddTransient<IUnitOfWork, DefaultUnitOfWork>();
        }
    }
}
