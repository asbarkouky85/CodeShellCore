using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet
{
    public interface IFileReader
    {
        bool FileExists(string path);
        string GetFileName(string path);
        string GetFolderFullName(string path);
        List<string> GetAllLines(string path);
        void WriteAllLines(string filePath, List<string> contents);
        string ReadAllText(string publishFile);
        void WriteAllText(string publishFile, string contents);
    }
}
