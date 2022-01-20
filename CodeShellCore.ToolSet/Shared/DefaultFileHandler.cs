using CodeShellCore.Helpers;
using CodeShellCore.ToolSet.Nuget;
using System;
using System.IO;

namespace CodeShellCore.ToolSet
{
    public class DefaultFileHandler : IToolSetFileHandler
    {
        private string path;
        bool _isFile;

        public DefaultFileHandler(string nugetPath, bool isFile = false)
        {
            this.path = nugetPath;
            _isFile = isFile;
            if (!_isFile)
            {
                if (!Directory.Exists(nugetPath))
                    Directory.CreateDirectory(nugetPath);
            }

        }

        public byte[] GetFile()
        {
            return File.ReadAllBytes(path);
        }

        public string GetFileName()
        {
            return Path.GetFileName(path);
        }

        public bool SaveFile(string fileName, byte[] file, out string message)
        {
            try
            {
                if (_isFile)
                {
                    Utils.CreateFolderForFile(path);
                    File.WriteAllBytes(path, file);
                }
                else
                {
                    File.WriteAllBytes(Path.Combine(path, fileName), file);
                }
                
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
            string dist = Path.Combine(path, fi.Name);
            var existing = Path.Combine(path, projectName, version);
            var exists = Directory.Exists(existing);
            if (!exists)
                File.Copy(packagePath, dist, true);
            return !exists;
        }
    }
}
