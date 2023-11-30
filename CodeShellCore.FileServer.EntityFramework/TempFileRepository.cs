using CodeShellCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.FileServer
{
    public class TempFileRepository : KeyRepository<TempFile, FileServerDbContext, long>, ITempFileRepository
    {
        public TempFileRepository(FileServerDbContext con) : base(con)
        {
        }

        public async Task<TempFile> GetWithCategory(long id)
        {
            var q = Loader.Include(e => e.AttachmentCategory);
            return await q.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TempFile> GetWithChunks(long id)
        {

            var q = Loader.AsNoTracking().Include(e => e.Chunks);
            return await q.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> IsChunksComplete(long id)
        {

            return await Loader.Where(e => e.Id == id).Select(e => e.TotalChunkCount == e.Chunks.Count).FirstOrDefaultAsync();
        }
    }
}
