using CodeShellCore.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Files.Uploads
{
    public class TempFileDto : TmpFileData
    {
        public long Id { get; set; }
        public long AttachmentTypeId { get; set; }
    }
}
