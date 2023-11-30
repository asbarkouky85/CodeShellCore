using System;

namespace CodeShellCore.FileServer
{
    public class UploadedFileInfo
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public int? Size { get; set; }
    }
}
