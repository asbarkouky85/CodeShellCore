using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet
{
    public class PhysicalFileReader : IFileReader
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public List<string> GetAllLines(string path)
        {
            return File.ReadAllLines(path).ToList();
        }

        public string GetFileName(string path)
        {
            var inf = new FileInfo(path);
            return inf.Name;
        }

        public string GetFolderFullName(string path)
        {
            var inf = new FileInfo(path);
            return inf.Directory.FullName.ToFolderPath();
        }

        public string ReadAllText(string publishFile)
        {
            return File.ReadAllText(publishFile);
        }

        public void WriteAllLines(string filePath, List<string> contents)
        {
            File.WriteAllLines(filePath, contents);
        }

        public void WriteAllText(string publishFile, string contents)
        {
            File.WriteAllText(publishFile, contents);
        }
    }
}
