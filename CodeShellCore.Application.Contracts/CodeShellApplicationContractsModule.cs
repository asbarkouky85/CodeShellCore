using CodeShellCore.Modularity;
using CodeShellCore.Proxy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore
{
    [DependsOn(
        typeof(CodeShellDomainSharedModule)
        )]
    public class CodeShellApplicationContractsModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddTransient<ISchemasGenerationService, SchemasGenerationService>();

            context.Services.AddTransient<ITypeScriptGenerationService, TypeScriptGenerationService>();
        }
    }
}
