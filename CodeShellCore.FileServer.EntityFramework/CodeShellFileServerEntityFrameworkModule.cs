using CodeShellCore.Modularity;
using CodeShellCore;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Extensions.DependencyInjection;

namespace CodeShellCore.FileServer
{
    [DependsOn(
        typeof(CodeShellEntityFrameworkModule),
		typeof(CodeShellFileServerDomainModule)
        )]
    public class CodeShellFileServerEntityFrameworkModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddUnitOfWork<FileServerUnit, IFileServerUnit>();
            context.Services.AddGenericRepository(typeof(Repository_Int64<,>));
            context.Services.AddCodeshellDbContext<FileServerDbContext>();
            context.Services.AddRepositoryFor<Attachment, AttachmentRepository, IAttachmentRepository>();
            context.Services.AddRepositoryFor<AttachmentCategory, AttachmentCategoryRepository, IAttachmentCategoryRepository>();
            context.Services.AddRepositoryFor<TempFile, TempFileRepository, ITempFileRepository>();
        }
    }
}