using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Sql
{
    public class SqlRestoreRequest 
    {
        public string DbName { get; set; }
        public string BackupPath { get; set; }
        public string ConnectionString { get; set; }
        

        
    }
}
