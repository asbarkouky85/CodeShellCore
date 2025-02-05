using CodeShellCore.Modularity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Notifications
{
    [DependsOn(
        typeof(CodeShellDomainModule)
        )]
    public class CodeShellNotificationsDomainModule : CodeShellModule
    {

        public override void RegisterServices(CodeshellAppContext context)
        {
            
        }
    }
}
