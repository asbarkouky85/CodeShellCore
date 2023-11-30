using CodeShellCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeShellCore.FileServer
{
    public class AttachmentRepository : KeyRepository<Attachment, FileServerDbContext, long>, IAttachmentRepository
    {
        public AttachmentRepository(FileServerDbContext con) : base(con)
        {
        }

        Task<IQueryable<UploadedFileInfo>> _queryInfo()
        {

            var q = Loader.Select(e => new UploadedFileInfo
            {
                Id = e.Id,
                FileName = e.FileName,
                Size = e.Size
            });
            return Task.FromResult(q);
        }

        public async Task<List<UploadedFileInfo>> GetInfo(IEnumerable<long> id)
        {
            var q = await _queryInfo();
            return await q.Where(e => id.Contains(e.Id)).ToListAsync();
        }

        public async Task<UploadedFileInfo> GetInfo(long id)
        {
            var q = await _queryInfo();
            return await q.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public Task<Attachment> GetWithCategory(Expression<Func<Attachment, bool>> find)
        {
            return Loader.Include(e => e.AttachmentCategory).FirstOrDefaultAsync(find);
        }
    }
}
