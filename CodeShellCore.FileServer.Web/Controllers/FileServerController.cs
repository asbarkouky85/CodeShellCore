using CodeShellCore.Data.Helpers;
using CodeShellCore.Files;
using CodeShellCore.FileServer.Business;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.FileServer.Web.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class FileServerController : BaseApiController, IAttachmentFileService
    {
        IAttachmentFileService service => GetService<IAttachmentFileService>();

        public virtual FileBytes GetBytes(long id) => service.GetBytes(id);
        public virtual string GetFileName(long id) => service.GetFileName(id);
        public virtual FileBytes GetTempBytes(string path) => service.GetTempBytes(path);
        public virtual SubmitResult SaveAttachment([FromBody] SaveAttachmentRequest req) => service.SaveAttachment(req);
        public virtual IEnumerable<TmpFileData> Upload([FromForm] UploadRequestDto dto)
        {
            dto.Files = new List<FileBytes>();

            foreach (var f in Request.Form.Files)
            {
                using (MemoryStream str = new MemoryStream())
                {
                    f.CopyTo(str);
                    dto.Files.Add(new FileBytes(f.FileName, str.ToArray()));
                }
            }
            return service.Upload(dto);
        }
        public SubmitResult ValidateFile([FromBody] FileValidationRequest req) => service.ValidateFile(req);

        public FileResult GetFile(long id)
        {
            var b = service.GetBytes(id);
            return File(b.Bytes, b.MimeType);
        }

        public FileResult GetTempFile(string path)
        {
            var b = service.GetTempBytes(path);
            return File(b.Bytes, b.MimeType);
        }
    }
}
