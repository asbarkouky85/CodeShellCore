using CodeShellCore.Files.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.AuditLogs
{
    public static class AuditLogger
    {
        public static Logger Log { get; private set; }
        internal static void Initialize()
        {
            if (Log == null)
            {
                Log = Logger.Create(Shell.ProjectAssembly.GetName().Name, "Audit");
            }
        }
    }
}
