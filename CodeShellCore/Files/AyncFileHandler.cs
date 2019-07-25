using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CodeShellCore.Files
{
    public class AsyncFileHandler
    {
        private Thread WritingThread;
        private List<string> WritingBuffer;
        private bool Writing = false;

        private static List<WriterByPath> Writers = new List<WriterByPath>();

        public event EventHandler<int> AfterWriting;
        public string FilePath { get; private set; }
        
        private AsyncFileHandler(string fileName)
        {
            FilePath = fileName;
            if (!File.Exists(fileName))
            {
                FileStream st = File.Create(fileName);
                st.Close();
            }
            WritingBuffer = new List<string>();
        }

        public static AsyncFileHandler GetWriter(string path)
        {
            WriterByPath p = Writers.Where(d => d.FilePath == path).FirstOrDefault();
            if (p == null)
            {
                p = new WriterByPath
                {
                    FilePath = path,
                    Writer = new AsyncFileHandler(path)
                };
                p.Writer.Start();
            }
            return p.Writer;
        }

        public int Start()
        {

            int lines = CountLines();

            WritingThread = new Thread(WritingFunction);
            WritingThread.Name = "LoggerThread";
            WritingThread.IsBackground = true;
            WritingThread.Start();

            return lines;
        }

        public void Append(string txt)
        {
            lock (this)
            {
                if (Writing)
                    Monitor.Wait(this);

                WritingBuffer.Add(txt);
                Monitor.Pulse(this);
            }
        }

        public string Read()
        {
            string st = "";
            lock (this)
            {
                if (Writing)
                    Monitor.Wait(this);

                if (File.Exists(FilePath))
                    st = File.ReadAllText(FilePath);
                Monitor.Pulse(this);
            }
            return st;
        }

        public void Clear()
        {
            lock (this)
            {
                if (Writing)
                    Monitor.Wait(this);

                File.WriteAllText(FilePath, "");
                Monitor.Pulse(this);
            }
        }

        public int CountLines()
        {
            int lines = 0;
            lock (this)
            {
                lines = File.ReadAllLines(FilePath).Length;
            }
            return lines;
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
            TextItem txt = new TextItem(value, this);
            Thread th = new Thread(txt.AppendFunction);
            th.IsBackground = true;
            th.Start();
        }

        protected void WritingFunction()
        {
            while (true)
            {
                lock (this)
                {
                    if (WritingBuffer.Count == 0)
                        Monitor.Wait(this);

                    Writing = true;
                    Monitor.Pulse(this);

                    try
                    {
                        using (StreamWriter wt = new StreamWriter(FilePath, true))
                        {
                            foreach (string st in WritingBuffer)
                            {
                                wt.WriteLine(st);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    AfterWriting?.Invoke(this, WritingBuffer.Count);

                    WritingBuffer = new List<string>();
                    Writing = false;
                    Monitor.Pulse(this);

                }
            }
        }
    }
}
