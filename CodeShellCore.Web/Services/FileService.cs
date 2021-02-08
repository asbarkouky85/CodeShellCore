using CodeShellCore.Files;
using CodeShellCore.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace CodeShellCore.Web.Services
{
    public class FileService : ServiceBase, IFileUploadService
    {
        public FileService()
        {

        }

        public virtual List<TmpFileData> Upload(Dictionary<string, IFormFile> files)
        {
            List<TmpFileData> lst = new List<TmpFileData>();

            foreach (var d in files)
            {

                using (MemoryStream str = new MemoryStream())
                {
                    d.Value.CopyTo(str);
                    byte[] byts = str.ToArray();
                    var bts = new FileBytes(d.Value.FileName, byts);
                    TmpFileData data = FileUtils.AddToTemp(bts,d.Key);
                    if (data != null)
                        lst.Add(data);
                }
            }
            return lst;
        }

        //public FileBytes GetFile(long id)
        //{

        //}
    }
}
