using CodeShellCore.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer.Business
{
    public class SaveAttachmentRequest : TempFileDto
    {
        public bool SaveInDb { get; set; }
    }
}
