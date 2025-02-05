using CodeShellCore.Modularity;
using CodeShellCore;

namespace CodeShellCore.Notifications
{
    [DependsOn(
        typeof(CodeShellDomainSharedModule)
        )]
    public class CodeShellNotificationsDomainSharedModule : CodeShellModule
    {
    }
}