using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Files
{
    public interface IFileInfo
    {
        string Extension { get; }
        int? Size { get; }
    }
}
