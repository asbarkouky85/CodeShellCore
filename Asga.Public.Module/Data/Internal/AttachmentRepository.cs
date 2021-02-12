using CodeShellCore.Data.Attachments;
using CodeShellCore.Files.Uploads;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Data.Internal
{
    public class AttachmentRepository : DefaultAttachmentRepository<Attachment, AsgaPublicContext>
    {
        public AttachmentRepository(AsgaPublicContext con, IUploadedFilesHandler _uploaded) : base(con, _uploaded)
        {
        }
    }
}
