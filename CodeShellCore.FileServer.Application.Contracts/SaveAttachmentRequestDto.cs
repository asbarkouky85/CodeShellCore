using CodeShellCore.Files;

namespace CodeShellCore.FileServer
{
    public class SaveAttachmentRequestDto : TempFileDto
    {
        public string ContainerName { get; set; }
        public string Folder { get; set; }
    }
}
