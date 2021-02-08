

using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.FileServer
{
    
    public partial class Attachment : FileServerBaseModel
    {
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public long AttachmentCategoryId { get; set; }
        public long? BinaryAttachmentId { get; set; }
        public virtual AttachmentCategory AttachmentCategory { get; set; }
        public virtual BinaryAttachment BinaryAttachment { get; set; }

    }
}
