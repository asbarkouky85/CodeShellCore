using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Sql
{
    public class SqlQueryRequest
    {
        public string ConnectionString { get; set; }
        public string SqlQuery { get; set; }

    }
}
