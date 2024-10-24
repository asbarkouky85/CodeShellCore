using CodeShellCore.Modularity;
using CodeShellCore;

namespace CodeShellCore.Reporting
{
    [DependsOn(
        typeof(CodeShellDomainSharedModule)
        )]
    public class CodeShellReportingModule : CodeShellModule
    {
    }
}