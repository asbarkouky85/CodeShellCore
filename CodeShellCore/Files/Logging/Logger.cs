using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using CodeShellCore.Helpers;

namespace CodeShellCore.Files.Logging
{
    public class Logger
    {
        #region Private Fields

        private string _FilePath;
        private string App;
        private TimeSpan DeleteLimit { get { return new TimeSpan(DaysToKeepFiles, 0, 0, 0); } }

        #endregion

        #region Static

        
        private static string LocationsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\fileLocations.xml";
        private static LocationCollection LocationCollection = LocationCollection.GetCollection(LocationsFilePath);
        private static Logger _default;

        private static Logger Default
        {
            get
            {
                if (_default == null) _default = new Logger("TraceLog");
                return _default;
            }
        }

        public static TracerWriter Out { get { return Default.TextWriter; } }
        public static Dictionary<string, FileLocation> FileLocations { get { return LocationCollection.Dictionary; } }
        #endregion

        #region Public Properties
        public TracerWriter TextWriter { get; private set; }
        [DefaultValue(7)]
        public int DaysToKeepFiles { get; set; }
        public int LineCount { get; private set; }
        public string FolderPath { get; private set; }
        public string FileName { get { return (new System.IO.FileInfo(_FilePath)).Name; } } 
        #endregion

        #region Constructor
        private Logger(string app, string folder = null)
        {
            App = app;

            FileLocation f = LocationCollection.GetFileLocation(app, folder);

            FolderPath = f.FolderPath;
            _FilePath = f.FilePath;
            try
            {
                Utils.CreateFolderForFile(_FilePath);
                StartFile();
            }
            catch { }

        }
        #endregion

        #region Static Methods

        public static Logger Create(string AppName, string logFolder)
        {
            return new Logger(AppName, logFolder);
        }

        public static void Set(string AppName, string logFolder)
        {
            _default = new Logger(AppName, logFolder);
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
            TextWriter?.WriteLine(st);
        }

        public void WriteLogException(Exception e, string st = null)
        {
            
            if (st != null)
                WriteLogLine(st);

            WriteLogLine(e.Message);
            WriteLogLine("Exception Type : " + e.GetType().Name);
            string[] arr = e.StackTrace.Split(new char[] { '\n' });

            foreach (string str in arr)
                WriteLogLine(str.Replace('\r', ' '));
            
        }
        
        void StartFile()
        {
            TextWriter = new TracerWriter(_FilePath);
            LineCount = TextWriter.Handler.CountLines();
            TextWriter.Handler.AfterWriting += FileObj_AfterWriting;
        }

        void FileObj_AfterWriting(object sender, int count)
        {
            LineCount += count;
            if (LineCount >= 4000)
            {
                _FilePath = Path.Combine(FolderPath, LocationCollection.NewFileName());
                TextWriter.ChangeFile(_FilePath);

                LocationCollection.Dictionary[App].FilePath = _FilePath;
                LocationCollection.Save();

                LineCount = 0;
                Cleanup();
            }
        }

        void Cleanup()
        {
            try
            {
                foreach (string file in Directory.GetFiles(FolderPath))
                {
                    FileInfo inf = new FileInfo(file);
                    if ((DateTime.Now - inf.CreationTime) >= DeleteLimit)
                    {
                        File.Delete(file);
                    }
                }
            }
            catch { }
        }
    }
}
