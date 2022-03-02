using CodeShellCore.Data.Attachments;
using CodeShellCore.Files;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asga.Public
{
    public partial class Attachment : IAttachmentModel
    {
        [NotMapped]
        public TmpFileData File { get; set; }

        public void LoadFile(string serviceUrl)
        {
            File = new TmpFileData(FileUtils.GetUploadedFileUrl(FilePath));
        }
    }
}
