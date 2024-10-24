using CodeShellCore.Modularity;
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
    }
}
