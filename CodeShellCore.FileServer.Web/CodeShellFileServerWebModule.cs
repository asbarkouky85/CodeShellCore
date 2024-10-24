using CodeShellCore.Modularity;
using CodeShellCore.Web;

namespace CodeShellCore.FileServer
{
    [DependsOn(
        typeof(CodeShellFileServerApplicationContractsModule),
		typeof(CodeShellWebModule)
        )]
    public class CodeShellFileServerWebModule : CodeShellModule
    {
    }
}