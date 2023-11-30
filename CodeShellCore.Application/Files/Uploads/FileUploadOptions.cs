using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Files.Uploads
{
    public class FileUploadOptions
    {
        public bool Authorize { get; set; }
        public BlobContainerConfiguration Default { get; set; }
        public List<BlobContainerConfiguration> Containers { get; set; }
    }
}
