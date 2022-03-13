using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Sql
{
    public class RestoreDbCommand
    {
        public string DbName { get; set; }
        public string BackupPath { get; set; }
    }
}
