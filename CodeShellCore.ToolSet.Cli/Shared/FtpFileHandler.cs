using CodeShellCore.Helpers;
using CodeShellCore.Text;
using System;
using System.IO;
using System.Linq;
using CodeShellCore.ToolSet.Ftp;

namespace CodeShellCore.ToolSet
{
    public class FtpFileHandler : IToolSetFileHandler
    {
        string folder;
        ToolSetFTPClient cl;
        public FtpFileHandler(string path, bool isFile = false)
        {
            cl = ToolSetFTPClient.FromString(path, out folder);

        }

        public byte[] GetFile()
        {
            var f = cl.DownloadFile(folder);
            if (f.IsSuccess)
            {
                return f.Bytes;
            }
            else
            {
                Console.WriteLine(f.ExceptionMessage);
                return new byte[0];
            }
        }

        public string GetFileName()
        {
            return folder.GetAfterLast("/");
        }

        public bool SaveFile(string fileName, byte[] file, out string message)
        {
            var s = cl.UploadFile(file, Utils.CombineUrl(folder, fileName));
            message = s.ExceptionMessage;
            return s.IsSuccess;
        }

        public bool UploadPackage(string projectName, string version, string packagePath)
        {
            var existing = cl.GetDirectoryList(folder);
            
            string path = Utils.CombineUrl(folder, projectName);
            var dir = new string[] { };
            if (existing.Contains(projectName))
            {
                dir = cl.GetDirectoryList(path);
            }
            var exists = dir.Contains(version);
            if (!exists)
            {
                byte[] byts = File.ReadAllBytes(packagePath);
                string dist = Utils.CombineUrl(folder, Path.GetFileName(packagePath));
                cl.UploadFile(byts, dist);
            }
            return !exists;
        }
    }
}
