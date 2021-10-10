using CodeShellCore.Files;
using CodeShellCore.Files.Uploads;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Attachments
{
    public interface IHasUploads
    {
        void LoadFile(IUploadedFilesHandler handl, string serviceUrl = null);
    }
}
