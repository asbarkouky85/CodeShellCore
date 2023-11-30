using CodeShellCore.Data;
using CodeShellCore.Data.EntityFramework;
using System;

namespace CodeShellCore.FileServer.Data
{
    public class FileServerUnit : UnitOfWork<FileServerDbContext>, IFileServerUnit
    {
        protected override Type GenericRepositoryType => typeof(Repository_Int64<,>);
        public FileServerUnit(IServiceProvider provider) : base(provider)
        {
        }

        public IAttachmentRepository AttachmentRepository => GetRepository<IAttachmentRepository>();

        public IAttachmentCategoryRepository AttachmentCategoryRepository => GetRepository<IAttachmentCategoryRepository>();
        public ITempFileRepository TempFileRepository => GetRepository<ITempFileRepository>();
    }
}
