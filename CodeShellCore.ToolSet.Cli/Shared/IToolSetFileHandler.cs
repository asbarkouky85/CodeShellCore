using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet
{
    public interface IToolSetFileHandler
    {
        bool UploadPackage(string projectName, string version, string packagePath);

        byte[] GetFile();
        string GetFileName();
        bool SaveFile(string fileName, byte[] file, out string message);
    }
}
