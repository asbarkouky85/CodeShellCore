using CodeShellCore.Modularity;
using CodeShellCore;
using CodeShellCore.MQ;
using CodeShellCore.FileServer.Paths;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.FileServer
{
    [DependsOn(
        typeof(CodeShellFileServerApplicationContractsModule),
		typeof(CodeShellApplicationModule),
		typeof(CodeShellFileServerDomainModule),
		typeof(CodeShellRabbitMqModule)
        )]
    public class CodeShellFileServerApplicationModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddTransient<IAttachmentFileService, AttachmentFileService>();
            context.Services.AddTransient<IInternalAttachmentFileService, AttachmentFileService>();
            //context.Services.AddTransient<IUploadedFilesHandler, AttachmentFileService>();
            context.Services.AddTransient<IPathProvider, PathProvider>();

            context.Services.AddAutoMapper(typeof(CodeshellFileServerAutoMapperProfile).Assembly);
        }
    }
}