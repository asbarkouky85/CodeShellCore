using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Files
{
    public class CroppedTmpFileData : TmpFileData
    {
        public CropPixels Rectangle { get; set; }
    }
}
