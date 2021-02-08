using CodeShellCore.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer.Business
{
    public class TempFileDto : TmpFileData
    {
        public long Id { get; set; }
        public long AttachmentTypeId { get; set; }
    }
}
