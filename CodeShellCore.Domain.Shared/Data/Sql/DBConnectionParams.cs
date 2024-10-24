using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Sql
{
    public class DbConnectionParams
    {
        private string _connectionString;
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public DbConnectionParams(string connectionString = null)
        {
            _connectionString = connectionString;
        }
        public string ConnectionString
        {
            get { return _connectionString ?? $"Server={Server};User Id={UserId};Password={Password};" + (Database == null ? "" : $"Database={Database}"); }
            set { _connectionString = value; }
        }
    }
}
