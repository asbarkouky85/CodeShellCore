using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Public.Data.Internal
{
    public class AttachmentRepository : CodeShellCore.Data.Attachments.DefaultAttachmentRepository<Attachment, AsgaPublicContext>
    {
        public AttachmentRepository(AsgaPublicContext con) : base(con)
        {
        }
    }
}
