using CodeShellCore.Data;
using CodeShellCore.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer.Data
{
    public class FileServerUnit : UnitOfWork<FileServerDbContext>, IFileServerUnit
    {
        protected override Type GenericRepositoryType => typeof(Repository_Int64<,>);
        public FileServerUnit(IServiceProvider provider) : base(provider)
        {
        }

        public IRepository<Attachment> AttachmentRepository => GetRepositoryFor<Attachment>();

        public IRepository<AttachmentCategory> AttachmentCategoryRepository => GetRepositoryFor<AttachmentCategory>();
    }
}
