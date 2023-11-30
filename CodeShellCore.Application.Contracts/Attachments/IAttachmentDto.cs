using CodeShellCore.Data;
using CodeShellCore.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Attachments
{
    public interface IAttachmentDto : IDetailObject<long>, IHasFileDto
    {
        string EntityType { get; set; }
        long EntityId { get; set; }
        string FilePath { get; set; }
    }
}
