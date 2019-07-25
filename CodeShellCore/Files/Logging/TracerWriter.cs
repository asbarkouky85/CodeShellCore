using System;
using System.Text;
using System.IO;
using CodeShellCore.Files;

namespace CodeShellCore.Files.Logging
{
    public class TracerWriter : TextWriter
    {
        public AsyncFileHandler Handler { get; private set; }
        public int LineCount { get { return Handler.CountLines(); } }

        string CurrentTimeString
        {
            get
            {
                DateTime now = DateTime.Now;
                return string.Format("[{0}:{1}:{2}.{3}]",
                    now.Hour.ToString("D2"),
                    now.Minute.ToString("D2"),
                    now.Second.ToString("D2"),
                    now.Millisecond.ToString("D3")
                    );
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }

        public TracerWriter(string fileName)
        {
            Handler = AsyncFileHandler.GetWriter(fileName);
        }

        public void ChangeFile(string fileName)
        {
            Handler = AsyncFileHandler.GetWriter(fileName);
        }

        public override void WriteLine(string value)
        {
            value = CurrentTimeString + " " + value;
            Handler.WriteLine(value);
        }




    }
}
