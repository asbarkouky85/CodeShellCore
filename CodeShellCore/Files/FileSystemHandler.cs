using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.Files
{
    public class FileSystemHandler : IFileHandler
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public void WriteAllBytes(string path, byte[] contents)
        {
            File.WriteAllBytes(path, contents);
        }

        public void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }

        public void CreateFolderForFile(string baseComponentPath)
        {
            Utils.CreateFolderForFile(baseComponentPath);
        }
    }
}
