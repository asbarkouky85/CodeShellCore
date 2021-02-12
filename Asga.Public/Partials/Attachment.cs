using CodeShellCore.Data.Attachments;
using CodeShellCore.Files;
using CodeShellCore.Files.Uploads;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asga.Public
{
    public partial class Attachment : IAttachmentModel
    {
        [NotMapped]
        public TmpFileData File { get; set; }

        public void LoadFile(IUploadedFilesHandler handl, string serviceUrl = null)
        {
            FilePath = handl.GetUrlByPath(FilePath);
            File = new TmpFileData(FilePath);
        }
    }
}
