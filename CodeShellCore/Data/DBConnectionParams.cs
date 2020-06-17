using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public class DbConnectionParams
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }

        public string ConnectionString { get { return $"Server={Server};User Id={UserId};Password={Password};" + (Database == null ? "" : $"Database={Database}"); } }
    }
}
