using CodeShellCore.Data;
using CodeShellCore.Data.Services;
using CodeShellCore.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Definitions
{
    public class MoldsterEnvironment
    {
        public string Name { get; set; }
        public DbConnectionParams ConnectionParams { get; set; }
        public string SourceDatabase { get; set; }
        public string ConfigDatabase { get; set; }
        public IEnumerable<string> Databases { get; set; }
        public UploadConfig Upload { get; set; }
        public IEnumerable<RestoreDbCommand> Restore { get; set; }

        public static MoldsterEnvironment Development => new MoldsterEnvironment { Upload = new UploadConfig { Type = "DEV" } };
    }
}
