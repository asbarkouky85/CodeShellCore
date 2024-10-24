using CodeShellCore.Modularity;
using CodeShellCore;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Files.Uploads;

namespace CodeShellCore.FileServer
{
    [DependsOn(
        typeof(CodeShellDomainModule),
		typeof(CodeShellFileServerDomainSharedModule)
        )]
    public class CodeShellFileServerDomainModule : CodeShellModule
    {
       
    }
}