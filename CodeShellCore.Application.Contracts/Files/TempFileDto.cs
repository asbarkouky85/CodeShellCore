using CodeShellCore.Files.Uploads;
using CodeShellCore.Text;
using System;
using System.Text.RegularExpressions;

namespace CodeShellCore.Files
{
    public class TempFileDto : ITempFileData
    {
        public string Id { get; set; }
        public long AttachmentTypeId { get; set; } = 1;
        public string Url { get; set; }
        public string FileTempPath { get; set; }
        public int? Size { get; set; }
        public string FileName { get; set; }
        public string UploadId { get; set; }
        public string MimeType { get; set; }

        public FileData ToFileData(string fieldName)
        {
            return new FileData
            {
                FieldName = fieldName,
                FileName = FileName,
                FullPath = FileTempPath
            };
        }

        public TempFileDto()
        {

        }

        public TempFileDto(string id) : this()
        {
            Url = id;
            Id = id;
        }

        public TempFileDto(string id, string fileName):this(id)
        {
            FileName = fileName;   
        }
    }
}
