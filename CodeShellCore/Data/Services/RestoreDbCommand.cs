using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Services
{
    public class RestoreDbCommand
    {
        public string DbName { get; set; }
        public string BackupPath { get; set; }
    }
}
