using CodeShellCore.Data.Helpers;
using CodeShellCore.Files;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.FileServer.Business
{
    public interface IAttachmentFileService
    {
        IEnumerable<TmpFileData> Upload(UploadRequestDto dto);
        SubmitResult ValidateFile(FileValidationRequest req);
        SubmitResult SaveAttachment(SaveAttachmentRequest req);
        FileBytes GetBytes(long id);
        FileBytes GetTempBytes(string path);
        string GetFileName(long id);
    }
}
