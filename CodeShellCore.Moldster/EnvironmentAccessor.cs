using CodeShellCore.Moldster.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster
{
    public class EnvironmentAccessor
    {
        public MoldsterEnvironment CurrentEnvironment { get; set; } = new MoldsterEnvironment();
    }
}
