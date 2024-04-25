

using CodeShellCore.Files;
using System.IO;
using System;

namespace CodeShellCore.FileServer
{
    public partial class Attachment : FileServerBaseModel
    {
        public long AttachmentCategoryId { get; protected set; }
        public int? Size { get; protected set; }
        public string ContainerName { get; protected set; }
        public string ContentType { get; protected set; }
        public string Extension { get; protected set; }
        public string FileName { get; protected set; }
        public string FullPath { get; protected set; }
        public long? BinaryAttachmentId { get; protected set; }
        public virtual AttachmentCategory AttachmentCategory { get; set; }

        public virtual AttachmentBinary BinaryAttachment { get; set; }

        public Attachment()
        {

        }

        public Attachment(long id, string fileName, long attachmentTypeId, string containerName = null)
        {
            Id = id;
            FileName = fileName;
            AttachmentCategoryId = attachmentTypeId;
            Extension = Path.GetExtension(FileName);
            ContentType = MimeData.GetFileMimeType(FileName);
            ContainerName = containerName;
        }

        public void SetSize(int? size)
        {
            Size = size;
        }
        public void SetBlobName(string blobName)
        {
            FullPath = blobName;
        }

    }
}
