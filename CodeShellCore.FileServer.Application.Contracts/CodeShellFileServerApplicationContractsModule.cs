using CodeShellCore.Modularity;
using CodeShellCore;

namespace CodeShellCore.FileServer
{
    [DependsOn(
        typeof(CodeShellApplicationContractsModule),
		typeof(CodeShellFileServerDomainSharedModule)
        )]
    public class CodeShellFileServerApplicationContractsModule : CodeShellModule
    {
    }
}