using System;
using System.IO;

namespace CodeShellCore.ToolSet.Versions
{
    public class ProjectVersionRequest 
    {
        public string MainDirectory { get; set; }
        public string Project { get; set; }
        public string Version { get; set; }
        public bool IsWeb { get; set; }
        public string PublishProfile { get; set; }

        public string GetShortVersion()
        {
            string[] parts = Version.Split(new char[] { '.' });
            string[] full = new string[] { "1", "0" };

            for (int i = 0; i < parts.Length; i++)
            {
                if (i >= full.Length)
                    break;
                full[i] = parts[i];
            }

            return string.Join(".", full);
        }

        public string GetLongVersionString()
        {
            string[] parts = Version.Split(new char[] { '.' });
            if (parts.Length >= 4)
                return Version;

            string[] full = new string[] { "1", "0", "0", "0" };

            for (int i = 0; i < parts.Length; i++)
            {
                if (i >= full.Length)
                    break;
                full[i] = parts[i];
            }
            return string.Join(".", full);
        }
    }
}
