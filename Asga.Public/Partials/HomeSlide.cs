using CodeShellCore.Data.Attachments;
using CodeShellCore.Files;
using CodeShellCore.Files.Uploads;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Asga.Public
{
    public partial class HomeSlide : IHasUploads
    {
        [NotMapped]
        public TmpFileData ImageFile { get; set; }

        public void LoadFile(IUploadedFilesHandler handl, string serviceUrl = null)
        {
            var path = handl.GetUrlByPath(Image);
            ImageFile = new TmpFileData(path);
        }
    }
}
