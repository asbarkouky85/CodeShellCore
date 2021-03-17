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
using CodeShellCore.Linq;
using CodeShellCore.Files.Uploads;

namespace CodeShellCore.FileServer.Business.Internal
{
    public class AttachmentFileService : UploadedFileHandler, IAttachmentFileService, IUploadedFilesHandler
    {
        private readonly IPathProvider _paths;
        private readonly IFileServerUnit unit;

        protected override string SaveRoot => _paths.RootFolderPath;
        protected override string TempRoot => _paths.TempFolder;
        protected virtual int DefaultAttachmentType => 1;

        public AttachmentFileService(IFileServerUnit unit, IPathProvider paths)
        {
            this.unit = unit;
            _paths = paths;
        }

        public virtual IEnumerable<TmpFileData> Upload(UploadRequestDto dto)
        {
            dto.AttachmentTypeId = dto.AttachmentTypeId == 0 ? DefaultAttachmentType : dto.AttachmentTypeId;
            var cat = unit.AttachmentCategoryRepository.FindSingle(dto.AttachmentTypeId);
            ValidateFiles(cat, dto.Files);

            List<TempFileDto> lst = new List<TempFileDto>();
            int i = 1;
            foreach (var d in dto.Files)
            {
                var tmpFile = AddToTemp(d, "file_" + (i++));
                var t = tmpFile.MapTo<TempFileDto>(false);
                t.AttachmentTypeId = cat.Id;
                lst.Add(t);
            }
            return lst;
        }

        protected virtual void ValidateFiles(AttachmentCategory cat, IEnumerable<IFileInfo> lst)
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

        public virtual SubmitResult SaveAttachment(SaveAttachmentRequest req)
        {
            req.AttachmentTypeId = req.AttachmentTypeId == 0 ? DefaultAttachmentType : req.AttachmentTypeId;
            var cat = unit.AttachmentCategoryRepository.FindSingle(req.AttachmentTypeId);

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

            string url = null;
            if (req.SaveInDb)
            {
                att.BinaryAttachment = new BinaryAttachment(dto.Bytes);
            }
            else
            {
                var root = _paths.RootFolderPath;
                var name = Guid.NewGuid();
                url = Utils.CombineUrl(cat.FolderPath ?? "default", name.ToString() + dto.Extension);
                att.FullPath = Path.Combine(root, url).Replace("/", "\\");

                Utils.CreateFolderForFile(att.FullPath);
                File.WriteAllBytes(att.FullPath, dto.Bytes);
            }

            unit.AttachmentRepository.Add(att);
            var s = unit.SaveChanges();
            s.Data["FileData"] = new SavedFileDto
            {
                Id = att.Id.ToString(),
                Path = url
            };
            return s;
        }

        public virtual FileBytes GetBytes(long id)
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

        public virtual SubmitResult ValidateFile(FileValidationRequest req)
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

        public virtual string GetFileName(long id)
        {
            var name = unit.AttachmentRepository.GetSingleValue(e => e.FileName, e => e.Id == id);
            if (name == null)
            {
                throw new ArgumentOutOfRangeException("not found");
            }
            return name;
        }

        public virtual FileBytes GetTempBytes(string path)
        {
            string filePath = Path.Combine(_paths.TempFolder, path);
            var b = new FileBytes(filePath);
            if (b.Size == null)
            {
                throw new ArgumentOutOfRangeException("not found");
            }
            return b;
        }

        public virtual FileBytes GetBytesByUrl(string path)
        {
            string filePath = Path.Combine(_paths.RootFolderPath, path);
            var b = new FileBytes(filePath);
            if (b.Size == null)
            {
                throw new ArgumentOutOfRangeException("not found");
            }
            return b;
        }

        public override bool SaveTemp(TmpFileData req, out SavedFileDto dto, long? type = null, string folder = null, bool db = false)
        {
            dto = null;
            if (req?.TmpPath != null)
            {
                var save = req.MapTo<SaveAttachmentRequest>(false);
                save.AttachmentTypeId = type ?? 0;
                save.SaveInDb = db;
                var att = SaveAttachment(save);
                if (att.IsSuccess && att.Data.TryGetValue("FileData", out object sv))
                {
                    dto = (SavedFileDto)sv;
                    return true;
                }
            }
            return false;
        }

        public override TmpFileData AddToTemp(FileBytes bts, string key)
        {
            var data = base.AddToTemp(bts, key);
            if (data != null)
            {
                data.Url = Utils.CombineUrl("fileserver/gettempfile?path=" + data.TmpPath);
            }
            return data;
        }

        public override string GetUrlById(string id)
        {
            if (id == null)
                return null;
            return Utils.CombineUrl("fileserver/getfile/" + id);
        }

        public override string GetUrlByPath(string path)
        {
            if (path == null)
                return null;
            return Utils.CombineUrl("fileserver/getbypath?path=" + path);
        }

        public override void DeleteTmp(TmpFileData tmp)
        {
            if (tmp.TmpPath != null)
            {
                var path = Path.Combine(_paths.TempFolder, tmp.TmpPath);
                if (File.Exists(path))
                    File.Delete(path);
            }
            
        }
    }
}
