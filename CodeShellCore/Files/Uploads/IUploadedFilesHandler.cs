using CodeShellCore.Data.Helpers;
using CodeShellCore.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Files.Uploads
{
    public interface IUploadedFilesHandler
    {
        bool SaveTemp(TmpFileData req, out SavedFileDto dto, long? type = null, string folder = null, bool db = false);
        void DeleteTmp(TmpFileData tmp);
        string GetUrlById(string id);
        string GetUrlByPath(string id);
        TmpFileData AddToTemp(FileBytes bts, string key);
    }
}
