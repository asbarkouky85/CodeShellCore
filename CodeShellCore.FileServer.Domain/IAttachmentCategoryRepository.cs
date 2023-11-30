using CodeShellCore.Data;
using System;
using System.Threading.Tasks;

namespace CodeShellCore.FileServer
{
    public interface IAttachmentCategoryRepository : IKeyRepository<AttachmentCategory,long>
    {
        public Task<bool> AnonymousDownloadAllowedByAttachmentId(long attachmentId);
        public Task<bool> DownloadAllowedByAttachmentId(string[] roles, long attachmentId);

        public Task<bool> AnonymousDownloadAllowed(long attachmentCategoryId);
        public Task<bool> DownloadAllowed(string[] roles, long attachmentCategoryId);
    }
}
