using CodeShellCore.Data.Helpers;
using CodeShellCore.Files;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace CodeShellCore.FileServer.Web.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class FileServerController : BaseApiController, IAttachmentFileService
    {
        IAttachmentFileService service;
        private readonly IInternalAttachmentFileService internalService;

        public FileServerController(IAttachmentFileService service, IInternalAttachmentFileService internalService)
        {
            this.service = service;
            this.internalService = internalService;
        }

        public Task<TempFileDto> ChunkUpload(ChunkUploadRequestDto dto)
        {
            return service.ChunkUpload(dto);
        }

        public Task<AttachmentCategoryDto> GetCategoryInfo(int id)
        {
            return service.GetCategoryInfo(id);
        }

        public Task<UploadedFileInfoDto> GetFileName(long id)
        {
            return service.GetFileName(id);
        }

        public Task<List<UploadedFileInfoDto>> GetFilesInfo(UploadedFileInfoRequestDto dto)
        {
            return service.GetFilesInfo(dto);
        }

        public Task<SubmitResult> SaveAttachment(SaveAttachmentRequestDto req)
        {
            return service.SaveAttachment(req);
        }

        public Task<SubmitResult> ValidateFile(FileValidationRequest req)
        {
            return service.ValidateFile(req);
        }

        public async Task<object> Get(string id)
        {
            var f = await internalService.GetBytes(id);
            return File(f.Bytes, f.MimeType, f.FileName);
        }

        public async Task<object> GetTemp(string path)
        {
            var f = await internalService.GetTempBytes(path);
            return File(f.Bytes, f.MimeType, f.FileName);
        }

        public Task<UploadResult> Upload([FromForm] UploadRequestDto dto) => (dto.Files == null) ? UploadMultiPart(dto.AttachmentTypeId) : internalService.Upload(dto);

        [Consumes("multipart/form-data")]
        public Task<UploadResult> UploadMultiPart(long catId)
        {
            var req = new UploadRequestDto
            {
                AttachmentTypeId = catId,
                Files = new List<FileBytes>()
            };

            foreach (var f in Request.Form.Files)
            {
                using MemoryStream str = new MemoryStream();
                f.CopyTo(str);
                var byts = new FileBytes(f.FileName, str.ToArray());
                if (byts.MimeType.ToLower().StartsWith("image"))
                {
                    try
                    {
                        using var im = Image.FromStream(str);
                        byts.Dimesion = new FileDimesion { Width = im.Width, Height = im.Height };
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
                req.Files.Add(byts);
            }
            return internalService.Upload(req);
        }
    }
}
