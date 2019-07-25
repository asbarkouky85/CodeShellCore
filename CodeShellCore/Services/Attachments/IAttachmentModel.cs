using CodeShellCore.Data;
using CodeShellCore.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Services.Attachments
{
    public interface IAttachmentModel : IEditable
    {
        TmpFileData File { get; set; }
        string EntityType { get; set; }
        long EntityId { get; set; }
        string FilePath { get; set; }

        void LoadFile(string serviceUrl);
    }
}
