using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using CodeShellCore.Services;
using CodeShellCore.Data.Services;
using CodeShellCore.FileServer.Data;
using CodeShellCore.Files;
using CodeShellCore.Data.Helpers;
using CodeShellCore.FileServer.Business;
using CodeShellCore.Helpers;
using CodeShellCore.FileServer.Paths;

namespace CodeShellCore.FileServer.Business.Internal
{
    public class AttachmentFileService : DataService<IFileServerUnit>, IAttachmentFileService
    {
        private readonly IPathProvider _paths;
        private readonly IFileServerUnit unit;

        public AttachmentFileService(IFileServerUnit unit, IPathProvider paths) : base(unit)
        {
            this.unit = unit;
            _paths = paths;
        }

        public IEnumerable<TmpFileData> Upload(UploadRequestDto dto)
        {
            var cat = unit.AttachmentCategoryRepository.FindSingle(dto.AttachmentTypeId);
            ValidateFiles(cat, dto.Files);


            List<TempFileDto> lst = new List<TempFileDto>();
            foreach (var d in dto.Files)
            {
                var name = Guid.NewGuid();
                var url = Utils.CombineUrl("Tmp", name + d.Extension);
                string path = Path.Combine(_paths.TempFolder, url);

                Utils.CreateFolderForFile(path);

                File.WriteAllBytes(path, d.Bytes);

                var tmpFile = new TempFileDto
                {
                    TmpPath = url,
                    Name = d.FileName,
                    AttachmentTypeId = dto.AttachmentTypeId,
                    Id = Utils.GenerateID(),
                    Size = d.Bytes.Length
                };

                using (MemoryStream str = new MemoryStream())
                {
                    lst.Add(tmpFile);
                }
            }
            return lst;
        }

        void ValidateFiles(AttachmentCategory cat, IEnumerable<IFileInfo> lst)
        {

            if (cat == null)
            {
                throw new ArgumentException("MSG_unknown_category");
            }

            foreach (var d in lst)
            {
                if (!cat.ValidateFile(d, out ValidationResult res))
                {
                    throw new ArgumentException(res.ErrorMessage);
                }
            }

        }

        public SubmitResult SaveAttachment(SaveAttachmentRequest req)
        {
            var cat = Unit.AttachmentCategoryRepository.FindSingle(req.AttachmentTypeId);

            var path = Path.Combine(_paths.TempFolder, req.TmpPath);
            var dto = new FileBytes(path);

            if (dto.Bytes == null)
                return new SubmitResult(1, "MSG_file_is_not_in_tmp");

            ValidateFiles(cat, new[] { dto });

            var att = new Attachment()
            {
                FileName = req.Name,
                AttachmentCategoryId = req.AttachmentTypeId
            };

            if (req.SaveInDb)
            {
                att.BinaryAttachment = new BinaryAttachment(dto.Bytes);
            }
            else
            {
                var root = _paths.RootFolderPath;
                var name = Guid.NewGuid();
                att.FullPath = Path.Combine(root, cat.FolderPath ?? "default", name.ToString() + dto.Extension).Replace("\\", "/");

                Utils.CreateFolderForFile(att.FullPath);
                File.WriteAllBytes(att.FullPath, dto.Bytes);
            }

            unit.AttachmentRepository.Add(att);
            return unit.SaveChanges();
        }

        public FileBytes GetBytes(long id)
        {
            var att = unit.AttachmentRepository.FindSingle(id);
            if (att == null)
            {
                throw new ArgumentOutOfRangeException("not found");
            }

            if (att.BinaryAttachmentId != null)
            {
                var s = unit.AttachmentRepository.GetSingleValue(e => e.BinaryAttachment, e => e.Id == id);
                return new FileBytes(att.FileName, s.Bytes);
            }
            else
            {
                var f = new FileBytes(att.FullPath);
                f.SetFileName(att.FileName);
                return f;
            }
        }

        public IEnumerable<TempFileDto> Upload()
        {
            throw new NotImplementedException();
        }

        public SubmitResult ValidateFile(FileValidationRequest req)
        {
            var res = new SubmitResult();
            var cat = unit.AttachmentCategoryRepository.FindSingle(req.AttachmentType);
            if (cat == null)
            {
                return new SubmitResult(1, "MSG_unknown_category");
            }

            if (!cat.ValidateFile(req, out ValidationResult val))
            {
                return new SubmitResult(1, val.ErrorMessage);
            }
            return res;
        }

        public string GetFileName(long id)
        {
            var name = unit.AttachmentRepository.GetSingleValue(e => e.FileName, e => e.Id == id);
            if (name == null)
            {
                throw new ArgumentOutOfRangeException("not found");
            }
            return name;
        }

        public FileBytes GetTempBytes(string path)
        {
            string filePath = Path.Combine(_paths.TempFolder, path);
            var b = new FileBytes(filePath);
            if (b.Size == null)
            {
                throw new ArgumentOutOfRangeException("not found");
            }
            return b;
        }
    }
}
