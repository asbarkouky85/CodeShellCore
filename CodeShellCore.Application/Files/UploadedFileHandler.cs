using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeShellCore.Files.Uploads
{
    public class UploadedFileHandler : IUploadedFilesHandler
    {
        static Regex UrlRegex = new Regex("^http(.*)://");
        protected virtual Dictionary<long, string> FolderByCategory { get; } = new Dictionary<long, string>();
        public virtual string TempRoot { get; private set; }
        public virtual string SaveRoot { get; private set; }

        public UploadedFileHandler()
        {
            TempRoot = Path.Combine(Shell.AppRootPath, Shell.PublicRoot);

            if (!string.IsNullOrEmpty(Shell.SharedPathRoot))
            {
                SaveRoot = Path.Combine(Shell.SharedPathRoot);
            }
            else
            {
                SaveRoot = Path.Combine(Shell.AppRootPath, Shell.PublicRoot);
            }
        }
        public virtual string GetUrlById(string id)
        {
            return id;
        }

        public virtual string GetUrlByPath(string id)
        {
            return id;
        }

        public void SaveTemp(ITempFileData req, long? type = null, string folder = null, bool db = false)
        {
            string fileName;
            if (req?.FileTempPath == null)
            {
                return;
            }

            folder = folder ?? "uploads";

            if (type != null && FolderByCategory.TryGetValue(type.Value, out string catFolder))
            {
                folder = catFolder;
            }

            if (req.FileName != null)
            {
                fileName = Utils.GenerateID().ToString() + "_" + req.FileName;
            }
            else
            {
                int startUrl = req.FileTempPath.Replace(Shell.PublicRoot + "", "").LastIndexOf('/') + 1;
                fileName = req.FileTempPath.Substring(startUrl, req.FileTempPath.Length - startUrl);
            }

            var path = Utils.CombineUrl(folder, fileName);

            string saveLocation = Path.Combine(SaveRoot, path).Replace("/", "\\");

            Utils.CreateFolderForFile(saveLocation);
            FileBytes bytes = null;

            if (UrlRegex.IsMatch(req.FileTempPath))
            {
                bytes = FileUtils.DownloadFile(req.FileTempPath);
                if (bytes != null)
                {
                    fileName = bytes.FileName;
                    var dir = Path.GetDirectoryName(path);
                    if (string.IsNullOrEmpty(dir))
                        dir = TempRoot;
                    bytes.Save(dir);
                }
            }
            else
            {
                string cur = Path.Combine(TempRoot, req.FileTempPath);
                if (File.Exists(path))
                    File.Delete(path);
                File.Copy(cur, saveLocation);
            }
        }

        public virtual void DeleteTmp(ITempFileData tmp)
        {
            if (tmp?.FileTempPath != null && File.Exists(tmp.FileTempPath))
                File.Delete(tmp.FileTempPath);
        }

        public virtual ITempFileData AddToTemp(FileBytes bts, string key)
        {
            if (bts.Size == null)
                return null;
            var id = Guid.NewGuid();
            var tmpPath = "Tmp/" + id.ToString() + bts.Extension;
            var tmpFullPath = Path.Combine(TempRoot, tmpPath);

            Utils.CreateFolderForFile(tmpFullPath);
            File.WriteAllBytes(tmpFullPath, bts.Bytes);
            return new TempFileDto
            {
                Url = tmpPath,
                Size = bts.Size ?? 0,
                FileTempPath = tmpPath,
                UploadId = key,
                FileName = bts.FileName,
                MimeType = bts.MimeType
            };
        }
    }
}
