using CodeShellCore.Files;
using CodeShellCore.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace CodeShellCore.Web.Services
{
    public class FileService : ServiceBase, IFileUploadService
    {
        private static string _tmpRoot = null;
        protected virtual string TmpRoot
        {
            get
            {
                if (_tmpRoot == null)
                {
                    _tmpRoot = Path.Combine(Shell.AppRootPath, "wwwroot/Tmp");
                    if (!Directory.Exists(_tmpRoot))
                        Directory.CreateDirectory(_tmpRoot);
                }
                return _tmpRoot;
            }
        }



        public virtual List<TmpFileData> Upload(Dictionary<string, IFormFile> files)
        {
            List<TmpFileData> lst = new List<TmpFileData>();

            foreach (var d in files)
            {
                string name = d.Key + "_" + d.Value.FileName;
                string path = Path.Combine(TmpRoot, name);

                using (MemoryStream str = new MemoryStream())
                {
                    d.Value.CopyTo(str);
                    byte[] byts = str.ToArray();
                    if (byts.Length > 0)
                        File.WriteAllBytes(path, byts);

                    lst.Add(new TmpFileData
                    {
                        Url = "Tmp/" + name,
                        Size = byts.Length,
                        TmpPath = path,
                        UploadId = name,
                        Name = d.Value.FileName,
                        MimeType = d.Value.ContentType

                    });
                }
            }
            return lst;
        }


    }
}
