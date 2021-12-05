using CodeShellCore.ToolSet.Nuget;
using System;
using System.IO;

namespace CodeShellCore.ToolSet
{
    public class DefaultFileHandler : IToolSetFileHandler
    {
        private string nugetPath;

        public DefaultFileHandler(string nugetPath, bool isFile = false)
        {
            this.nugetPath = nugetPath;
            if (!isFile)
            {
                if (!Directory.Exists(nugetPath))
                    Directory.CreateDirectory(nugetPath);
            }

        }

        public byte[] GetFile()
        {
            return File.ReadAllBytes(nugetPath);
        }

        public string GetFileName()
        {
            return Path.GetFileName(nugetPath);
        }

        public bool SaveFile(string fileName, byte[] file, out string message)
        {
            try
            {
                File.WriteAllBytes(Path.Combine(nugetPath, fileName), file);
                message = "Success";
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return true;
        }

        public bool UploadPackage(string projectName, string version, string packagePath)
        {
            FileInfo fi = new FileInfo(packagePath);
            string dist = Path.Combine(nugetPath, fi.Name);
            var existing = Path.Combine(nugetPath, projectName, version);
            var exists = Directory.Exists(existing);
            if (!exists)
                File.Copy(packagePath, dist, true);
            return !exists;
        }
    }
}
