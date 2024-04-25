using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeShellCore.FileServer
{
    public interface IAttachmentRepository : IRepository<Attachment>
    {
        
        Task<List<UploadedFileInfo>> GetInfo(IEnumerable<long> id);
        Task<UploadedFileInfo> GetInfo(long id);
        Task<Attachment> GetWithCategory(Expression<Func<Attachment, bool>> find);
    }
}
