using CodeShellCore.Files;

namespace CodeShellCore.FileServer.Business
{
    public class FileValidationRequest : IFileInfo
    {
        public int? Size { get; set; }
        public string Extension { get; set; }
        public int AttachmentType { get; set; }
        public string FileName { get; set; }
    }
}
