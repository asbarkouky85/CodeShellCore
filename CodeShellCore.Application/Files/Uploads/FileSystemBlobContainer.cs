using CodeShellCore.Helpers;
using CodeShellCore.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Files.Uploads
{
    public class FileSystemBlobContainer : IBlobContainer
    {
        
        private readonly BlobContainerConfiguration config;

        public FileSystemBlobContainer(BlobContainerConfiguration config)
        {
            this.config = config;
        }

        public Task DeleteAsync(string fullPath)
        {
            var filePath = Path.Combine(config.RootFolder, fullPath);
            Utils.CreateFolderForFile(filePath);
            if (File.Exists(filePath))
                File.Delete(filePath);
            return Task.CompletedTask;
        }

        public Task Finish(string name, string referenceId, Dictionary<int, string> chunkData)
        {
            var chunks = chunkData.OrderBy(e => e.Key).Select(e => e.Value).ToList();
            var root = config.RootFolder;
            var filePath = Path.Combine(root, name);
            Utils.CreateFolderForFile(filePath);
            File.Create(filePath).Close();
            using (var stream = File.Open(filePath, FileMode.Append, FileAccess.Write))
            {
                foreach (var chunk in chunks)
                {
                    var bytes = File.ReadAllBytes(Path.Combine(root, chunk));
                    foreach (var b in bytes)
                    {
                        stream.WriteByte(b);
                    }
                }
            }
            return Task.CompletedTask;
        }

        public Task<byte[]> GetAllBytesOrNullAsync(string name)
        {
            var filePath = Path.Combine(config.RootFolder, name);
            Utils.CreateFolderForFile(filePath);
            return File.ReadAllBytesAsync(filePath);
        }

        public string NormalizeName(string name)
        {
            return name;
        }

        public Task SaveAsync(string name, byte[] bytes)
        {
            var filePath = Path.Combine(config.RootFolder, name);
            Utils.CreateFolderForFile(filePath);
            if (File.Exists(filePath))
                File.Delete(filePath);

            return File.WriteAllBytesAsync(filePath, bytes);
        }

        public Task<string> StartFile(string name)
        {
            var uploadId = name.GetBeforeLast(".");
            var root = config.RootFolder;
            var savePath = Path.Combine(root, uploadId).Replace("\\", "/");
            Directory.CreateDirectory(savePath);
            return Task.FromResult(uploadId);
        }

        public Task<string> UploadPart(string name, string uploadId, int partNumber, byte[] bytes)
        {
            var root = config.RootFolder;
            var chunkId = Utils.CombineUrl(uploadId, partNumber.ToString());
            var savePath = Path.Combine(root, chunkId);
            Utils.CreateFolderForFile(savePath);
            File.WriteAllBytes(savePath, bytes);
            return Task.FromResult(chunkId);
        }
    }
}
