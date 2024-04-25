using CodeShellCore.Data;

namespace CodeShellCore.FileServer
{
    public class UploadedFileInfoDto : EntityDto<long>
    {
        public string FileName { get; set; }
        public int? Size { get; set; }
    }
}
