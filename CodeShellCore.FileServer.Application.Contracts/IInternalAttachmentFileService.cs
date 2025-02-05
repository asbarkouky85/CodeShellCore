using CodeShellCore.Files;
using System.Threading.Tasks;

namespace CodeShellCore.FileServer
{
    public interface IInternalAttachmentFileService
    {
        Task<FileBytes> GetBytes(string id);
        Task<FileBytes> GetTempBytes(string path);
        Task<UploadResult> Upload(UploadRequestDto dto);
        Task<UploadResult> UploadAndSave(UploadRequestDto req);
    }
}
