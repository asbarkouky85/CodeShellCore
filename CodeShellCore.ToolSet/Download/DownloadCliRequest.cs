using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.ToolSet.Download
{
    public class DownloadCliRequest
    {
        public string Url { get; set; }
        public string TargetFolder { get; set; }
        public string TargetFileName { get; set; }
    }
}
