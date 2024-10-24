using System;
using System.IO;
using System.IO.Compression;

namespace CodeShellCore.ToolSet.Zip
{
    public class ZipRequest
    {
        public string Target { get; set; }
        public string Source { get; set; }
        public string Pattern { get; set; }
        public bool? DeleteExisting { get; set; }
    }
}
