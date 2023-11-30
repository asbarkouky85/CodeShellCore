using CodeShellCore.Data;
using System.Threading.Tasks;

namespace CodeShellCore.FileServer
{
    public interface ITempFileRepository : IKeyRepository<TempFile, long>
    {
        
        Task<TempFile> GetWithCategory(long id);
        Task<TempFile> GetWithChunks(long id);
        
        Task<bool> IsChunksComplete(long id);
    }
}
