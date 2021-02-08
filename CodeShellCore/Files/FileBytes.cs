using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Files
{
    public class FileBytes : IFileInfo
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public byte[] Bytes { get; set; }
        public string Extension { get; set; }
        public int? Size => Bytes?.Length;
            
        public void Save(string folder)
        {
            File.WriteAllBytes(Path.Combine(folder, FileName), Bytes);
        }
        public FileBytes() { }
        public FileBytes(string name, byte[] bytes, string id = null)
        {
            FileName = name;
            Bytes = bytes;
            Extension = Path.GetExtension(name);
            MimeType = MimeData.GetFileMimeType(name);
        }

        public FileBytes(string filePath)
        {
            FileName = Path.GetFileName(filePath);
            Extension = Path.GetExtension(filePath);
            MimeType = MimeData.GetFileMimeType(FileName);
            if (File.Exists(filePath))
            {
                Bytes = File.ReadAllBytes(filePath);
            }
        }

        public void SetFileName(string name)
        {
            FileName = name;
            Extension = Path.GetExtension(name);
            MimeType = MimeData.GetFileMimeType(name);
        }

        public void SetMimeType(string mime)
        {
            MimeType = mime;
        }

        public string ToBase64String()
        {
            return Convert.ToBase64String(Bytes);
        }
    }
}
