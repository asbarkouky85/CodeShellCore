using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeShellCore.Files
{
    public class AsyncFileHandler : IDisposable
    {
        static List<WriterByPath> _writers = new List<WriterByPath>();

        /// <summary>
        /// Number of lines in the current file
        /// </summary>
        public long LineCount { get; private set; }
        /// <summary>
        ///  default 40000 when reached triggers <see cref="MaxLinesReached"/>
        /// </summary>
        public long MaxLines { get; set; } = 0;
        /// <summary>
        /// Triggered when <see cref="LineCount"/> count exeeds <see cref="MaxLines"/>
        /// </summary>
        public event EventHandler MaxLinesReached;
        public event EventHandler WritingFailed;
        public string FilePath { get; private set; }

        private AsyncFileHandler(string fileName)
        {
            FilePath = fileName;
        }

        public static AsyncFileHandler GetWriter(string path)
        {
            WriterByPath p = _writers.Where(d => d.FilePath == path).FirstOrDefault();
            if (p == null)
            {
                p = new WriterByPath
                {
                    FilePath = path,
                    Writer = new AsyncFileHandler(path)
                };
                _writers.Add(p);
            }
            return p.Writer;
        }

        public void ChangeFile(string fileName)
        {
            Stop();
            FilePath = fileName;
            Start();
        }

        public long Start()
        {

            LineCount = CountLines();
            return LineCount;
        }

        public void Stop() { }

        public string Read()
        {
            string st = "";
            lock (this)
            {
                if (File.Exists(FilePath))
                    st = File.ReadAllText(FilePath);
            }
            return st;
        }

        public void Clear()
        {
            lock (this)
            {
                File.WriteAllText(FilePath, "");
            }
        }

        public long CountLines()
        {
            var lineCounter = 0;
            if (!File.Exists(FilePath))
                return 0;
            lock (this)
            {
                try
                {
                    lineCounter = File.ReadAllLines(FilePath).Count();
                }
                catch
                {
                    return -1;
                }

            }
            return lineCounter;
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
            _append(value);
        }

        public void Dispose()
        {
            Stop();
        }

        private void _append(string txt)
        {
            lock (this)
            {
                try
                {
                    using (StreamWriter wt = new StreamWriter(FilePath, true))
                    {
                        wt.WriteLine(txt);
                        LineCount++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Writing Failed : " + e.GetMessageRecursive());
                    WritingFailed?.Invoke(this, new EventArgs());

                }
            }

            if (MaxLines != 0 && MaxLinesReached != null)
            {
                if (LineCount >= MaxLines && MaxLines != 0)
                {
                    MaxLinesReached?.Invoke(this, new EventArgs());
                }
            }
        }

    }
}
