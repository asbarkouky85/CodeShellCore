using CodeShellCore.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Files.Uploads
{
    public class SaveAttachmentRequest : TempFileDto
    {
        public bool SaveInDb { get; set; }
    }
}
