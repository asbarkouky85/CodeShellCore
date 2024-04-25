using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.EntityFramework.DesignTime
{
    public static class DesignTimeMigrationsAssemblies
    {
        public static Dictionary<string, string> Store { get; set; } = new Dictionary<string, string>();
    }
}
