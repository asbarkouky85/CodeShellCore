using CodeShellCore.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace CodeShellCore.FileServer
{
    public partial class AttachmentCategory : FileServerBaseModel
    {
        public AttachmentCategory()
        {
            Attachments = new HashSet<Attachment>();
        }

        public string Name { get; set; }
        public string ValidExtensions { get; set; }
        public int MaxSize { get; set; }
        public string FolderPath { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }

        public bool ValidateFile(IFileInfo bytes, out ValidationResult message)
        {
            message = null;

            if (!ValidExtensions.ToLower().Contains(bytes.Extension.ToLower()))
            {
                message = new ValidationResult("MSG_invalid_file_type", new[] { bytes.Extension, Name });
                return false;
            }

            if (MaxSize != 0 && bytes.Size > MaxSize)
            {
                message = new ValidationResult("MSG_file_exceeds_size", new[] { Name, (MaxSize / 1000).ToString("F2") });
                return false;
            }

            return true;
        }
    }
}
