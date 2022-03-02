using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer.Data
{
    public interface IFileServerUnit : IUnitOfWork
    {
        IRepository<Attachment> AttachmentRepository { get; }
        
        IRepository<AttachmentCategory> AttachmentCategoryRepository { get; }
    }
}
