using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Files
{
    public class WriterByPath
    {
        public string FilePath { get; set; }
        public AsyncFileHandler Writer { get; set; }
    }
}
