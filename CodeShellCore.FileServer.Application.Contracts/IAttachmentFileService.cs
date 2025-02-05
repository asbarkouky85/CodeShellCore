using CodeShellCore.Data.Helpers;
using CodeShellCore.Files;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.FileServer
{

    public interface IAttachmentFileService
    {
        Task<UploadResult> UploadBase64(UploadBase64RequestDto dto);
        Task<TempFileDto> ChunkUpload(ChunkUploadRequestDto dto);
        Task<SubmitResult> ValidateFile(FileValidationRequest req);
        Task<SubmitResult> SaveAttachment(SaveAttachmentRequestDto req);

        Task<UploadedFileInfoDto> GetFileName(long id);
        Task<List<UploadedFileInfoDto>> GetFilesInfo(UploadedFileInfoRequestDto dto);
        Task<AttachmentCategoryDto> GetCategoryInfo(int id);
    }
}
