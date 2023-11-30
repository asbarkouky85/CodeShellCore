using CodeShellCore.Files.Uploads;
using CodeShellCore.Helpers;
using CodeShellCore.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace CodeShellCore.Files
{
    public interface ITempFileData
    {

        string Id { get; set; }
        long AttachmentTypeId { get; set; }
        string Url { get; set; }
        string FileTempPath { get; set; }
        int? Size { get; set; }
        string FileName { get; set; }
        string UploadId { get; set; }
        string MimeType { get; set; }
        //FileData ToFileData(string fieldName);
        //void DeleteTmp();
        //string SaveFile(string folder, bool pubFolder = true);
    }
}
