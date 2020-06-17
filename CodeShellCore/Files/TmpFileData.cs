using CodeShellCore.Helpers;
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

        public string SaveFile(string folder, bool publicFolder = true)
        {
            folder = publicFolder ? Path.Combine(Shell.PublicRoot, folder) : folder;
            folder = folder.Replace("\\", "/");
            string dir = Path.Combine(Shell.AppRootPath, folder).Replace("\\", "/");
            FileBytes bytes = null;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string fileName = Utils.GenerateID().ToString() + "_" + Name;
            string nw = Path.Combine(dir, fileName);

            if (UrlRegex.IsMatch(TmpPath))
            {
                bytes = FileUtils.DownloadFile(TmpPath);
                if (bytes != null)
                {
                    fileName = bytes.FileName;
                    bytes.Save(folder);
                }
                else
                    return TmpPath;

            }
            else
            {
                string cur = Path.Combine(Shell.AppRootPath, TmpPath);
                if (File.Exists(nw))
                    File.Delete(nw);
                File.Move(cur, nw);
            }
            return Path.Combine(folder, fileName).Replace(Shell.PublicRoot + "/", "").Replace("\\", "/");
        }
    }
}
