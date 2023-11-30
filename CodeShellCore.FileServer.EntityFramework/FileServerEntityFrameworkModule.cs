using CodeShellCore.Data.EntityFramework;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Extensions.DependencyInjection;
using CodeShellCore.FileServer.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer
{
    public static class FileServerEntityFrameworkModule
    {
        public static void AddFileServerEntityFramework(this IServiceCollection services, bool asDefault = false, string migrationAssembly = null)
        {
            services.AddUnitOfWork<FileServerUnit, IFileServerUnit>();
            services.AddGenericRepository(typeof(Repository_Int64<,>));
            services.AddCodeshellDbContext<FileServerDbContext>(asDefault, migrationAssembly);
            services.AddRepositoryFor<Attachment, AttachmentRepository, IAttachmentRepository>();
            services.AddRepositoryFor<AttachmentCategory, AttachmentCategoryRepository, IAttachmentCategoryRepository>();
            services.AddRepositoryFor<TempFile, TempFileRepository, ITempFileRepository>();
            services.AddDataSeeders(typeof(IFileServerUnit).Assembly);
        }
    }
}
