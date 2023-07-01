using CodeShellCore.Files.Uploads;
using CodeShellCore.Helpers;
using CodeShellCore.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace CodeShellCore.Files
{
    public class TmpFileData
    {
        static Regex UrlRegex = new Regex("^http(.*)://");
        public string Url { get; set; }
        public string TmpPath { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }
        public string UploadId { get; set; }
        public string MimeType { get; set; }

        public FileData ToFileData(string fieldName)
        {
            return new FileData
            {
                FieldName = fieldName,
                FileName = Name,
                FullPath = TmpPath
            };
        }

        public TmpFileData()
        {

        }

        public TmpFileData(string url)
        {
            Url = url;
            Name = url?.GetAfterLast("/");
        }

        public void DeleteTmp()
        {
            FileUtils.DeleteTmp(this);
        }

        public virtual string SaveFile(string folder, bool pubFolder = true)
        {
            if (FileUtils.SaveTemp(this, folder, out SavedFileDto dto))
            {
                return dto.Path;
            }
            return null;
        }
    }
}
