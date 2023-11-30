using CodeShellCore.Files;
using CodeShellCore.Helpers;

namespace CodeShellCore.FileServer
{
    public class UploadResult 
    {
        public virtual bool Success => Code == "0";
        public string Code { get; set; } = "0";
        public string Message { get; set; }
        public string Details { get; set; }
        public TempFileDto[] Data { get; set; }
    }
}
