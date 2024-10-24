using CodeShellCore.Modularity;
using CodeShellCore;
using CodeShellCore.Files.Uploads;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.FileServer
{
    [DependsOn(
        typeof(CodeShellDomainSharedModule)
        )]
    public class CodeShellFileServerDomainSharedModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.Configure<FileUploadOptions>(context.Configuration.GetSection("Uploads"));
        }
    }
}