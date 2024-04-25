using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Files;
using CodeShellCore.Files.Uploads;
using CodeShellCore.FileServer.Paths;
using CodeShellCore.Helpers;
using CodeShellCore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeShellCore.FileServer
{
    public class AttachmentFileService : DataService<IFileServerUnit>, IAttachmentFileService, IInternalAttachmentFileService
    {
        private IPathProvider PathProvider => Store.GetRequiredService<IPathProvider>();
        private readonly IBlobContainerFactory _containerFactory;

        //protected IChunkUploaderFactory ChunkUploaderFactory => LazyServiceProvider.LazyGetRequiredService<IChunkUploaderFactory>();
        //private IRepository<TempFileChunk> ChunkRepo => Store.GetService<IRepository<TempFileChunk>>();
        private bool _authorize = true;


        private IAttachmentRepository Repository => Unit.AttachmentRepository;

        public AttachmentFileService(
            IOptions<FileUploadOptions> uploadOptions,
            IFileServerUnit unit,
            IBlobContainerFactory containerFactory) : base(unit)
        {
            // _paths = paths;
            _authorize = uploadOptions.Value.Authorize;
            _containerFactory = containerFactory;
        }

        public async Task<AttachmentCategoryDto> GetCategoryInfo(int id)
        {
            var repo = Unit.AttachmentCategoryRepository;
            var cat = await repo.FindAsync(id);
            return Mapper.Map(cat, new AttachmentCategoryDto());
        }

        public async Task<UploadedFileInfoDto> GetFileName(long id)
        {
            UploadedFileInfo inf = await Repository.GetInfo(id);
            if (inf == null)
            {
                return new UploadedFileInfoDto
                {
                    Id = id,
                    FileName = "(N/A)"
                };
            }
            return Mapper.Map(inf, new UploadedFileInfoDto());
        }

        public async Task<List<UploadedFileInfoDto>> GetFilesInfo(UploadedFileInfoRequestDto dto)
        {
            var data = await Repository.GetInfo(dto.Ids);
            return Mapper.Map(data, new List<UploadedFileInfoDto>());
        }

        public virtual async Task<TempFileDto> ChunkUpload(ChunkUploadRequestDto dto)
        {
            var cat = await Unit.AttachmentCategoryRepository.FindAsync(dto.AttachmentTypeId);
            IBlobContainer chunkUploader = _containerFactory.GetContainer(cat.ContainerName ?? "default");
            TempFile tmp;
            if (!dto.Id.HasValue)
            {
                ValidateFiles(dto.AttachmentTypeId, cat, new[] { dto });
                dto.Id = Utils.GenerateID();
                tmp = new TempFile(dto.Id.Value, dto.AttachmentTypeId, dto.FileName, dto.TotalChunkCount);
                string blobName = chunkUploader.NormalizeName(cat.GenerateBlobName(tmp));
                tmp.SetFullPath(blobName);
                tmp.SetFileSize(dto.Size);
                string referenceId = await chunkUploader.StartFile(tmp.FullPath);
                tmp.SetRefernceId(referenceId);
                await Unit.TempFileRepository.InsertAsync(tmp);
            }
            else
            {
                tmp = await Unit.TempFileRepository.FindAsync(dto.Id.Value);
            }

            var tmpDto = new TempFileDto
            {
                FileName = dto.FileName,
                AttachmentTypeId = dto.AttachmentTypeId,
                Id = dto.Id.ToString(),
                FileTempPath = tmp.FullPath
            };

            var bytes = Convert.FromBase64String(dto.Chunk);

            var chunkReference = await chunkUploader.UploadPart(tmp.FullPath, tmp.ReferenceId, dto.CurrentChunkIndex, bytes);
            var addedChunk = new TempFileChunk(dto.Id.Value, dto.CurrentChunkIndex, chunkReference);

            await Unit.GetRepositoryFor<TempFileChunk>().InsertAsync(addedChunk);
            await Unit.SaveChangesAsync();

            var complete = await Unit.TempFileRepository.IsChunksComplete(dto.Id.Value);

            if (complete)
            {
                var temp = await Unit.TempFileRepository.GetWithChunks(dto.Id.Value);
                var chunkData = temp.Chunks.ToDictionary(e => e.ChunkIndex, e => e.ReferenceId);
                await chunkUploader.Finish(temp.FullPath, tmp.ReferenceId, chunkData);
            }
            await Unit.SaveChangesAsync();
            return tmpDto;
        }

        public async Task<FileBytes> GetBytes(string stringId)
        {
            long.TryParse(stringId, out long id);

            await AuthorizeDownload(id);

            Attachment att = await Repository.GetWithCategory(e => e.Id == id);
            if (att == null)
            {
                throw new Exception("not found");
            }
            if (att.BinaryAttachmentId != null)
            {
                var s = Repository.GetSingleValue(e => e.BinaryAttachment, e => e.Id == id);
                return new FileBytes(att.FileName, s.Bytes);
            }
            else
            {
                var c = _containerFactory.GetContainer(att.ContainerName);
                byte[] b = await c.GetAllBytesOrNullAsync(att.FullPath);
                if (b == null)
                {
                    throw new Exception(Strings.Message("MSG__Not_found_on_provider"));
                }
                var f = new FileBytes(att.FileName, b);
                return f;
            }
        }

        public async Task<FileBytes> GetTempBytes(string path)
        {
            if (long.TryParse(path, out long id))
            {
                var tmpFile = await Unit.TempFileRepository.GetWithCategory(id);
                if (tmpFile == null)
                {
                    throw new Exception("not found");
                }
                var c = _containerFactory.GetContainer(tmpFile.AttachmentCategory.ContainerName);
                await AuthorizeDownloadByCategory(tmpFile.AttachmentCategoryId);
                var str = await c.GetAllBytesOrNullAsync(tmpFile.FullPath);
                var b = new FileBytes(tmpFile.FileName, str);
                return b;
            }
            throw new Exception("not found");
        }

        public virtual async Task<SubmitResult> SaveAttachment(SaveAttachmentRequestDto req)
        {
            var cat = await Unit.AttachmentCategoryRepository.FindAsync(req.AttachmentTypeId);
            var id = Utils.GenerateID();
            long.TryParse(req.Id, out id);
            var tmpFile = Unit.TempFileRepository.FindSingle(e => e.Id == id);
            if (tmpFile == null)
            {
                return new SubmitResult { Message = Strings.Message("MSG_file_is_not_in_tmp", req.FileName) };
            }

            var att = new Attachment(id, req.FileName, req.AttachmentTypeId, cat.ContainerName);
            IBlobContainer cont = _containerFactory.GetContainer(cat.ContainerName);
            att.SetSize(tmpFile.Size);
            att.SetBlobName(tmpFile.FullPath);
            await Repository.InsertAsync(att);
            await Unit.TempFileRepository.DeleteAsync(tmpFile);
            var res= await Unit.SaveChangesAsync();
            return res;
        }

        public virtual async Task<UploadResult> Upload(UploadRequestDto dto)
        {
            var cat = await Unit.AttachmentCategoryRepository.FindAsync(dto.AttachmentTypeId);
            ValidateFiles(dto.AttachmentTypeId, cat, dto.Files);

            List<TempFileDto> lst = new List<TempFileDto>();
            var tmContainer = _containerFactory.GetContainer(cat.ContainerName);

            foreach (var file in dto.Files)
            {
                try
                {
                    //if (!MagicNumbersData.ValidateMagic(file.Extension, file.Bytes))
                    //{
                    //    throw new Exception(Strings.Message("MSG__Content_does_not_match_a_{0}_file", file.Extension));
                    //}
                    var id = Utils.GenerateID();
                    var tmp = new TempFile(id, dto.AttachmentTypeId, file.FileName);
                    var blobName = cat.GenerateBlobName(tmp);
                    tmp.SetFullPath(blobName);
                    tmp.SetFileSize(file.Size);
                    await tmContainer.SaveAsync(blobName, file.Bytes);
                    var tmpDto = new TempFileDto
                    {
                        FileTempPath = tmp.FullPath,
                        FileName = file.FileName,
                        AttachmentTypeId = dto.AttachmentTypeId,
                        Id = id.ToString(),
                        Size = file.Size
                    };

                    lst.Add(tmpDto);
                    await Unit.TempFileRepository.InsertAsync(tmp);
                }
                catch 
                {

                    throw;
                }
            }
            await Unit.SaveChangesAsync();
            return new UploadResult
            {
                Data = lst.ToArray()
            };
        }

        public async Task<SubmitResult> ValidateFile(FileValidationRequest req)
        {
            var res = new SubmitResult();
            var cat = await Unit.AttachmentCategoryRepository.FindAsync(req.AttachmentType);
            ValidateFiles(req.AttachmentType, cat, new[] { req });
            return res;
        }

        private string GetCategoryName(AttachmentCategory cat)
        {
            return cat.Name;
        }

        private void AuthorizeUpload(AttachmentCategory cat)
        {
            if (!_authorize)
                return;
            if (!Unit.UserAccessor.IsUser)
            {
                if (!cat.AllowAnonymousUpload())
                    throw new Exception(Strings.Message("MSG_unauthorized_upload", GetCategoryName(cat)));
            }
            else
            {
                //if (!cat.CanUpload(Unit.UserAccessor.get))
                //    throw new Exception(Strings.Message("MSG_unauthorized_upload", GetCategoryName(cat)));
            }
        }

        private async Task AuthorizeDownload(long attachmentId)
        {
            if (!_authorize)
                return;
            if (Unit.UserAccessor.User?.UserId == null)
            {
                if (!await Unit.AttachmentCategoryRepository.AnonymousDownloadAllowed(attachmentId))
                {
                    throw new Exception(Strings.Message("MSG__Unauthorized_download"));
                }
            }
            else
            {
                //if (!await Unit.AttachmentCategoryRepository.DownloadAllowed(CurrentUser.Roles, attachmentId))
                //{
                //    throw new Exception(L["MSG__Unauthorized_download"]);
                //}
            }
        }

        private async Task AuthorizeDownloadByCategory(long attachmentCategoryId)
        {
            if (!_authorize)
                return;
            if (!Unit.UserAccessor.IsUser)
            {
                if (!await Unit.AttachmentCategoryRepository.AnonymousDownloadAllowed(attachmentCategoryId))
                {
                    throw new Exception(Strings.Message("MSG__Unauthorized_download"));
                }
            }
            else
            {
                //if (!await Unit.AttachmentCategoryRepository.DownloadAllowed(CurrentUser.Roles, attachmentCategoryId))
                //{
                //    throw new Exception(Strings.Message("MSG__Unauthorized_download"));
                //}
            }
        }

        private bool HasDoubleExtension(string fileName)
        {
            var regex = new Regex("\\.");

            var coll = regex.Matches(fileName);
            return coll.Count > 1;
        }

        private void ValidateFiles(long id, AttachmentCategory cat, IEnumerable<IFileInfo> lst)
        {

            if (cat == null)
            {
                throw new Exception(Strings.Message("MSG_unknown_category", id.ToString()));
            }

            AuthorizeUpload(cat);

            if (cat.MaxCount.HasValue && lst.Count() > cat.MaxCount)
            {
                throw new Exception(Strings.Message("MSG_file_count_exceeds_maximum_allowed", GetCategoryName(cat), cat.MaxCount?.ToString()));
            }

            foreach (var d in lst)
            {
                if (HasDoubleExtension(d.FileName))
                {
                    throw new Exception(Strings.Message("MSG__File_can_not_have_two_dots_in_its_name"));
                }
                if (!cat.ValidateFile(d, out ValidationResult res))
                {
                    var pars = new List<object> { GetCategoryName(cat) };
                    pars.AddRange(res.MemberNames?.Select(e => e).ToArray());

                    throw new Exception(Strings.Message(res.ErrorMessage, pars.Select(e => e.ToString()).ToArray()));
                }
            }
        }

        public virtual async Task<UploadResult> UploadAndSave(UploadRequestDto req)
        {
            var cat = await Unit.AttachmentCategoryRepository.FindAsync(req.AttachmentTypeId);

            List<TempFileDto> lst = new List<TempFileDto>();

            ValidateFiles(req.AttachmentTypeId, cat, req.Files);

            foreach (var f in req.Files)
            {
                var dto = new FileBytes(f.FileName, f.Bytes);
                var att = new Attachment(Utils.GenerateID(), f.FileName, req.AttachmentTypeId, cat.ContainerName);
                IBlobContainer cont = _containerFactory.GetContainer(cat.ContainerName);

                var blobName = cat.GenerateBlobName(att);
                att.SetBlobName(blobName);

                await cont.SaveAsync(blobName, dto.Bytes);
                await Repository.InsertAsync(att);

                lst.Add(new TempFileDto
                {
                    FileTempPath = blobName,
                    FileName = f.FileName,
                    AttachmentTypeId = req.AttachmentTypeId,
                    Id = att.Id.ToString(),
                    Size = att.Size
                });
            }
            await Unit.SaveChangesAsync();
            return new UploadResult
            {
                Data = lst.ToArray()
            };
        }


    }
}
