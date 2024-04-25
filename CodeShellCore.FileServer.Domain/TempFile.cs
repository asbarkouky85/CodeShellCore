using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodeShellCore.FileServer
{
    public class TempFile : FileServerBaseModel
    {
        public long AttachmentCategoryId { get; protected set; }
        public int? Size { get; protected set; }
        public string ContentType { get; protected set; }
        public string Extension { get; protected set; }
        public string FileName { get; protected set; }
        public string FullPath { get; protected set; }
        public string ReferenceId { get; protected set; }
        public int? TotalChunkCount { get; protected set; }
        public ICollection<TempFileChunk> Chunks { get; protected set; }

        public virtual AttachmentCategory AttachmentCategory { get; set; }

        protected TempFile()
        {
            Chunks = new HashSet<TempFileChunk>();
        }

        public TempFile(long id, long attachmentCategory, string fileName) : this()
        {
            Id = id;
            FileName = fileName;
            AttachmentCategoryId = attachmentCategory;
            Extension = Path.GetExtension(FileName);
            ContentType = MimeData.GetFileMimeType(FileName);
        }

        public TempFile(long id, int attachmentCategory, string fileName, int totalChunkCount) : this(id, attachmentCategory, fileName)
        {
            TotalChunkCount = totalChunkCount;

        }

        public void SetRefernceId(string refernceId)
        {
            ReferenceId = refernceId;
        }

        public void SetFullPath(string blobName)
        {
            FullPath = blobName;
        }

        public string GetBlobName()
        {
            return FullPath.GetBeforeLast(".");
        }

        public void SetFileSize(int? size)
        {

            Size = size;
        }
    }
}
