using CodeShellCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.FileServer
{
    public class AttachmentCategoryRepository : KeyRepository<AttachmentCategory,FileServerDbContext, long>, IAttachmentCategoryRepository
    {
        public AttachmentCategoryRepository(FileServerDbContext con) : base(con)
        {
        }

        public async Task<bool> AnonymousDownloadAllowedByAttachmentId(long attachmentId)
        {
            var res = await DbContext.Attachments
                .Where(e => e.Id == attachmentId)
                .Select(e => e.AttachmentCategory.AnonymousDownload || e.AttachmentCategory.AttachmentCategoryPermissions.Any(e => e.Role == "Anonymous" && e.Download))
            .FirstOrDefaultAsync();
            return res;
        }

        public async Task<bool> DownloadAllowedByAttachmentId(string[] roles, long attachmentId)
        {
            if (roles.Length == 0)
                return false;
            
            var res = await DbContext.Attachments
                .Where(e => e.Id == attachmentId)
                .Select(e => e.AttachmentCategory.AnonymousDownload || !e.AttachmentCategory.AttachmentCategoryPermissions.Any(e => e.Role == roles[0] && !e.Download))
            .FirstOrDefaultAsync();
            return res;
        }

        public async Task<bool> AnonymousDownloadAllowed(long attachmentTypeId)
        {
            
            var res = await DbContext.AttachmentCategories
                .Where(e => e.Id == attachmentTypeId)
                .Select(e => e.AnonymousDownload || e.AttachmentCategoryPermissions.Any(e => e.Role == "Anonymous" && e.Download))
            .FirstOrDefaultAsync();
            return res;
        }

        public async Task<bool> DownloadAllowed(string[] roles, long attachmentTypeId)
        {
            if (roles.Length == 0)
                return false;
            var res = await DbContext.AttachmentCategories
                .Where(e => e.Id == attachmentTypeId)
                .Select(e => e.AnonymousDownload || !e.AttachmentCategoryPermissions.Any(e => e.Role == roles[0] && !e.Download))
            .FirstOrDefaultAsync();
            return res;
        }
    }
}
