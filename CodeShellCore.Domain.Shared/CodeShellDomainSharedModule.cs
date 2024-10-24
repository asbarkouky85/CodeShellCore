using CodeShellCore.Modularity;
using CodeShellCore.MultiTenant;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore
{
    [DependsOn(typeof(CodeShellCoreModule))]
    public class CodeShellDomainSharedModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddTransient<IClientProvider, DefaultClientProvider>();
            context.Services.AddTransient<ITokenGenerator, TokenGenerator>();
            context.Services.AddTransient<IAuthorizationService, AuthenticatedOnlyAuthorizationService>();
            context.Services.AddTransient<IUserDataService, UserDataService>();
            context.Services.AddTransient<ITenantDataProvider, NullTenantDataProvider>();
            context.Services.AddOptions<CodeShellSecurityOptions>("Security");
            context.Services.Configure<CodeShellSecurityOptions>(e => new CodeShellSecurityOptions());
        }
    }
}
