using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Files
{
    public class DecompressOptions
    {
        public bool Replace { get; set; } = true;
        public string[] KeepFiles { get; set; }
        public string[] ForceReplaceFiles { get; set; }
    }
}
