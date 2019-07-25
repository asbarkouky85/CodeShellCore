using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Files
{
    public class FileBytes
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public byte[] Bytes { get; set; }
        public string Extension { get; set; }

        public void Save(string folder)
        {
            File.WriteAllBytes(Path.Combine(folder, FileName), Bytes);
        }

        public string ToBase64String()
        {
            return Convert.ToBase64String(Bytes);
        }
    }
}
