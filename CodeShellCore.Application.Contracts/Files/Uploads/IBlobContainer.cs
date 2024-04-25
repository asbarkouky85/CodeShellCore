using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Files.Uploads
{
    public interface IBlobContainer
    {
        Task DeleteAsync(string fullPath);
        Task Finish(string fullPath, string referenceId, Dictionary<int, string> chunkData);
        Task<byte[]> GetAllBytesOrNullAsync(string fullPath);
        string NormalizeName(string v);
        Task SaveAsync(string blobName, byte[] bytes);
        Task<string> StartFile(string fullPath);
        Task<string> UploadPart(string fullPath, string referenceId, int currentChunkIndex, byte[] bytes);
    }
}