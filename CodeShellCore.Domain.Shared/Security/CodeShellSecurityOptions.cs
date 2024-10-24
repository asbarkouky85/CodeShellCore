using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authentication
{
    public class CodeShellSecurityOptions
    {
        public string TokenProvider { get; set; }
        public TimeSpan? TokenLifeTime { get; set; } = new TimeSpan(1, 0, 0, 0);
    }
}
