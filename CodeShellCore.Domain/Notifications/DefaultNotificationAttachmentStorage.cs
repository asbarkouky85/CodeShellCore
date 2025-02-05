using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Net;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    public class DefaultNotificationAttachmentStorage : INotificationAttachmentStorage
    {
        private string _storageFolder = "_attachments";
        public DefaultNotificationAttachmentStorage() { }
        public Task<long> CreateFileFromBase64(string fileName, string base64)
        {
            return Task.Run(() =>
            {
                var bytes = Convert.FromBase64String(base64);
                var info = new FileInfo(fileName);
                var id = Utils.GenerateID();
                var filePath = Path.Combine(Shell.AppRootPath, $"{_storageFolder}\\{id}__{fileName}.{info.Extension}");
                Utils.CreateFolderForFile(filePath);
                File.WriteAllBytes(filePath, bytes);
                return id;
            });
        }

        public Task<FileBytes> GetFile(long attachmentId)
        {
            return Task.Run(() =>
            {
                var folder = Path.Combine(Shell.AppRootPath, _storageFolder);
                var filePath = Directory.GetFiles(folder, $"{attachmentId}__*").FirstOrDefault();
                if (filePath != null)
                {
                    var splt = filePath.Split(new[] { attachmentId + "__" }, StringSplitOptions.RemoveEmptyEntries);
                    var info = new FileInfo(splt[1]);
                    var bytes = File.ReadAllBytes(filePath);
                    return new FileBytes($"{info.Name}.{info.Extension}", bytes);
                }
                throw new FileNotFoundException();
            });
        }
    }
}
