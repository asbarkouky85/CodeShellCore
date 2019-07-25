using System;
using System.Collections.Generic;
using System.IO;

using CodeShellCore.Text;
using CodeShellCore.Files.Logging;
using CodeShellCore.Helpers;
using Newtonsoft.Json;
using CodeShellCore.Services;

namespace CodeShellCore.Files.Storage
{
    public class FileStorageService : ServiceBase
    {
        AsyncFileHandler Writer;
        string FilePath;
        List<CachedItem> PendingAdd;
        int CurrentLine;
        int StartingLine;

        public FileStorageService(string fileLocation, bool isRelativePath = true)
        {
            try
            {
                FilePath = isRelativePath ? Path.Combine(Shell.AppRootPath, fileLocation) : fileLocation;
                Utils.CreateFolderForFile(FilePath);
                Writer = AsyncFileHandler.GetWriter(FilePath);
                PendingAdd = new List<CachedItem>();

                if (!File.Exists(FilePath))
                {
                    StartingLine = 0;
                    CurrentLine = 0;
                    
                }
                else
                {
                    CurrentLine = StartingLine = GetCurrentLine();
                }
            }
            catch (Exception Exc)
            {

                Logger.WriteException(Exc);
            }
        }

        private int GetCurrentLine()
        {
            return File.ReadAllLines(FilePath).Length;
        }

        public List<CachedItem> GetContents()
        {

            string contents = Writer.Read();
            if (!string.IsNullOrEmpty(contents))
                return contents.FromJson<List<CachedItem>>();

            return new List<CachedItem>();
        }

        public int Add(object item, Exception exception = null)
        {
            int cur = CurrentLine;
            CurrentLine++;
            PendingAdd.Add(new CachedItem(item, exception));
            return cur;
        }

        public void Delete(int index)
        {
            List<CachedItem> lst = GetContents();
            lst.RemoveAt(index);
        }

        public void Clear()
        {
            Writer.Clear();
        }

        public void Save()
        {

            var tsk = GetContents();
            List<CachedItem> lst = GetContents();
            foreach (var x in PendingAdd)
            {
                lst.Add(x);
            }
            CurrentLine = lst.Count;
            StartingLine = lst.Count;
            PendingAdd.Clear();
            string data = lst.ToJson(Formatting.Indented);
            Writer.Clear();
            Writer.WriteLine(data);

        }
    }
}
