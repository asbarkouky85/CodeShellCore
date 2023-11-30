using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Files.Images
{
    public class CroppedTmpFileDto : TempFileDto
    {
        public CropPixels Rectangle { get; set; }
    }
}
