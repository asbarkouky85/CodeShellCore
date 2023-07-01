using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Files
{
    public interface IFileHandler
    {
        bool Exists(string path);
        void WriteAllText(string path, string contents);
        void WriteAllBytes(string path, byte[] contents);
        void CreateFolderForFile(string baseComponentPath);
    }
}
