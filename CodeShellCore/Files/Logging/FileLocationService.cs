using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CodeShellCore.Files.Logging
{
    public class FileLocation
    {
        private static string PublicFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);

        public string AppName { get; set; }
        public string FolderPath { get; private set; }
        public string FilePath { get; private set; }

        private FileLocation(string app, string folder)
        {
            AppName = app;
            FolderPath = UseLogFolder(folder);
            if (FolderPath != null)
            {
                FilePath = GetLatestLogFile();
            }
        }

        private string GetLatestLogFile()
        {
            var d = new DirectoryInfo(FolderPath);
            var f = d.GetFiles("*.log").OrderByDescending(e => e.LastWriteTimeUtc).FirstOrDefault();
            if (f == null)
            {
                return NewFilePath();
            }
            return f.FullName;
        }

        public static FileLocation Make(string app, string folder)
        {
            if (string.IsNullOrEmpty(folder))
                return null;
            var loc = new FileLocation(app, folder);
            if (loc.FolderPath == null || loc.FilePath == null)
                return null;
            return loc;
        }

        public void MoveToNewFile()
        {
            FilePath = NewFilePath();
        }

        private string NewFilePath()
        {
            return Path.Combine(FolderPath, NewFileName());
        }

        static int FileId = 1;

        private string NewFileName()
        {
            DateTime t = DateTime.Now;
            FileId += 1;
            string fileName = t.ToString("yyyy_MM_dd_hh_mm_ss_") + FileId.ToString("D3") + ".log";
            return fileName;
        }

        private string TryCreateFolder(string folder, bool tryPublic = true)
        {
            try
            {
                Directory.CreateDirectory(folder);
                return folder;
            }
            catch
            {
                if (tryPublic)
                {
                    string f = Path.Combine(PublicFolder, AppName);
                    return TryCreateFolder(f, false);
                }
                return null;
            }
        }

        private string UseLogFolder(string folder)
        {
            if (Directory.Exists(folder))
                return folder;
            return TryCreateFolder(folder);
        }
    }
}
