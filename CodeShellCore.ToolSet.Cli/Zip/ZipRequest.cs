using System;
using System.IO;
using System.IO.Compression;

namespace CodeShellCore.ToolSet.Zip
{
    public class ZipRequest 
    {
        public string TargetLocation { get; set; }
        public string FolderLocation { get; set; }
        public string Pattern { get; set; }
    }
}
