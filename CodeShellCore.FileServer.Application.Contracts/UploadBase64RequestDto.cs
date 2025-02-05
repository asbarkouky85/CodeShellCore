using CodeShellCore.Files;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer
{

    public class UploadBase64RequestDto
    {
        public long AttachmentTypeId { get; set; }
        public List<Base64FileDto> Files { get; set; }
    }

    public class Base64FileDto
    {
        public string FileName { get; set; }
        public string Content { get; set; }

        private byte[] _bytes;
        public byte[] GetBytesOnce()
        {
            if (_bytes == null)
            {
                _bytes = Convert.FromBase64String(Content);
            }
            return _bytes;
        }

        public FileValidationRequest GetFileInfo()
        {
            return new FileValidationRequest
            {
                Size = GetBytesOnce().Length,
                Extension = FileName.GetAfterLast("."),
                FileName = FileName,
            };
        }
    }
}

