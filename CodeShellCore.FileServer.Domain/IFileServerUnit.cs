using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.FileServer
{
    public interface IFileServerUnit : IUnitOfWork
    {
        public IAttachmentRepository AttachmentRepository { get; }

        public IAttachmentCategoryRepository AttachmentCategoryRepository => GetRepository<IAttachmentCategoryRepository>();
        public ITempFileRepository TempFileRepository => GetRepository<ITempFileRepository>();
    }
}
