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
            SaveRoot = Path.Combine(Shell.AppRootPath, Shell.PublicRoot);
        }
        public virtual string GetUrlById(string id)
        {
            return id;
        }

        public virtual string GetUrlByPath(string id)
        {
            return id;
        }

        public virtual bool SaveTemp(TmpFileData req, out SavedFileDto dto, long? type = null, string folder = null, bool db = false)
        {
            dto = new SavedFileDto();
            try
            {
                string fileName;
                if (req?.TmpPath == null)
                {
                    return false;
                }

                folder = folder ?? "uploads";

                if (type != null && FolderByCategory.TryGetValue(type.Value, out string catFolder))
                {
                    folder = catFolder;
                }

                if (req.Name != null)
                {
                    fileName = Utils.GenerateID().ToString() + "_" + req.Name;
                }
                else
                {
                    int startUrl = req.TmpPath.Replace(Shell.PublicRoot + "", "").LastIndexOf('/') + 1;
                    fileName = req.TmpPath.Substring(startUrl, req.TmpPath.Length - startUrl);
                }

                dto.Path = Utils.CombineUrl(folder, fileName);
                //req.TmpPath = Path.Combine(SaveRoot, dto.Path);

                string saveLocation = Path.Combine(SaveRoot, dto.Path).Replace("/", "\\");

                Utils.CreateFolderForFile(saveLocation);
                FileBytes bytes = null;

                if (UrlRegex.IsMatch(req.TmpPath))
                {
                    bytes = FileUtils.DownloadFile(req.TmpPath);
                    if (bytes != null)
                    {
                        fileName = bytes.FileName;
                        var dir = Path.GetDirectoryName(dto.Path);
                        if (string.IsNullOrEmpty(dir))
                            dir = TempRoot;
                        bytes.Save(dir);
                    }
                    else
                        return false;

                }
                else
                {
                    string cur = Path.Combine(TempRoot, req.TmpPath);
                    if (File.Exists(dto.Path))
                        File.Delete(dto.Path);
                    File.Copy(cur, saveLocation);
                }
                return true;
            }
            catch
            {

                return false;
            }
        }

        public virtual void DeleteTmp(TmpFileData tmp)
        {
            if (tmp?.TmpPath != null && File.Exists(tmp.TmpPath))
                File.Delete(tmp.TmpPath);
        }

        public virtual TmpFileData AddToTemp(FileBytes bts, string key)
        {
            if (bts.Size == null)
                return null;
            var id = Guid.NewGuid();
            var tmpPath = "Tmp/" + id.ToString() + bts.Extension;
            var tmpFullPath = Path.Combine(TempRoot, tmpPath);

            Utils.CreateFolderForFile(tmpFullPath);
            File.WriteAllBytes(tmpFullPath, bts.Bytes);
            return new TmpFileData
            {
                Url = tmpPath,
                Size = bts.Size ?? 0,
                TmpPath = tmpPath,
                UploadId = key,
                Name = bts.FileName,
                MimeType = bts.MimeType
            };
        }
    }
}
