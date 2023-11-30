using CodeShellCore.Files;
using System.Collections.Generic;

namespace CodeShellCore.FileServer
{
    public class UploadRequestDto
    {
        public long AttachmentTypeId { get; set; }
        public IList<FileBytes> Files { get; set; }


    }
}
