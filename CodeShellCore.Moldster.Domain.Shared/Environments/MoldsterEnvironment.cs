using CodeShellCore.Data.Sql;
using CodeShellCore.Net;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Environments
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
