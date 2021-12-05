using System.Collections.Generic;

namespace CodeShellCore.Files.CsProject
{
    public interface ICsProjectFileReader
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
