using CodeShellCore.Files;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    public interface INotificationAttachmentStorage
    {
        Task<long> CreateFileFromBase64(string fileName, string base64);
        Task<FileBytes> GetFile(long attachmentId);
    }
}