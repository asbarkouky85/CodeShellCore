using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using CodeShellCore.Helpers;
using System.Linq;
using CodeShellCore.Cli;

namespace CodeShellCore.Files.Logging
{
    public class Logger : IDisposable, IOutputWriter
    {
        public static Logger Default { get; private set; }
        public FileLocation Location { get; private set; }
        public AsyncFileHandler TextWriter { get; private set; }
        public int DaysToKeepFiles { get; set; } = 7;
        public string ClassName { get; set; }

        private TimeSpan DeleteLimit { get { return new TimeSpan(DaysToKeepFiles, 0, 0, 0); } }
        private bool _dateAdded;
        string CurrentTimeString
        {
            get
            {
                DateTime now = DateTime.Now;
                string milli = now.Millisecond.ToString("D3");
                _dateAdded = true;
                return now.ToString("dd/MM/yyyy HH:mm:ss." + milli);
            }
        }

        private Logger(string app, string folder = null)
        {

            folder = string.IsNullOrEmpty(folder) ? Path.Combine(Shell.AppRootPath, "Logs") : folder;
            Location = FileLocation.Make(app, folder);
            _startFile();
        }


        #region Static Methods

        public static Logger Create(string AppName, string logFolder)
        {
            return new Logger(AppName, logFolder);
        }

        public static void Set(string AppName, string logFolder = null)
        {
            Default = new Logger(AppName, logFolder);
        }

        public static void WriteLine(object ob)
        {
            Default?.WriteLogLine(ob?.ToString());
        }

        public static void WriteLine(string st)
        {
            Default?.WriteLogLine(st);
        }

        public static void WriteException(Exception e, string st = null)
        {
            Default?.WriteLogException(e, st);
        }

        #endregion

        public void WriteLogLine(string st)
        {
            if (!_dateAdded)
                st = "[" + CurrentTimeString + "]" + (ClassName != null ? $"[{ClassName}]" : "") + " " + st;

            TextWriter?.WriteLine(st);
            _dateAdded = false;
        }

        public void Write(string st, bool replaceLast = false)
        {
            if (!_dateAdded)
                st = "[" + CurrentTimeString + "]" + (ClassName != null ? $"[{ClassName}]" : "") + " " + st;
            TextWriter?.Write(st);
        }

        public void WriteLogException(Exception e, string st = null)
        {

            if (st != null)
                WriteLogLine(st);

            WriteLogLine(e.GetMessageRecursive());
            WriteLogLine("Exception Type : " + e.GetType().Name);
            string[] arr = new string[0];
            if (e is AggregateException && e.InnerException != null)
            {
                WriteLogLine("Inner Exception " + e.InnerException.GetType().Name);
                if (!string.IsNullOrEmpty(e.InnerException.StackTrace))
                    arr = e.InnerException.StackTrace?.Split(new char[] { '\n' });
            }
            else if (!string.IsNullOrEmpty(e.StackTrace))
            {
                arr = e.StackTrace.Split(new char[] { '\n' });
            }

            foreach (string str in arr)
                WriteLogLine(str.Replace('\r', ' '));

        }

        private void _startFile()
        {
            TextWriter = AsyncFileHandler.GetWriter(Location.FilePath);
            TextWriter.MaxLines = 4000;
            TextWriter.MaxLinesReached += OnMaxLines;
            TextWriter.WritingFailed += OnMaxLines;
            if (TextWriter.Start() == -1)
            {
                OnMaxLines(TextWriter, new EventArgs());
            }
        }

        private void OnMaxLines(object sender, EventArgs args)
        {
            Location.MoveToNewFile();
            TextWriter.ChangeFile(Location.FilePath);
        }

        void _cleanUp()
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(Location.FolderPath);
                var deleteables = d.GetFiles().Where(inf => DateTime.Now - inf.CreationTime >= DeleteLimit).ToList();
                foreach (var file in deleteables)
                {
                    File.Delete(file.FullName);
                }
            }
            catch { }
        }

        public void Dispose()
        {
            TextWriter?.Dispose();
        }

        public void WriteLine(bool replaceLast = false)
        {
            WriteLogLine("");
        }



        public ColorSetter Set(ConsoleColor yellow)
        {
            return ColorSetter.Set(yellow);
        }

        public void WriteLine(string v, bool replaceLast = false)
        {
            WriteLogLine(v);
        }

        public void GotoColumn(int column)
        {
            Write("\t");
        }
    }
}
