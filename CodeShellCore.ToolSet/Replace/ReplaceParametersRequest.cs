using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.ToolSet.Replace
{
    public class ReplaceParametersRequest
    {
        public string Parameters { get; set; }
        public string ParameterFile { get; set; }
        public string TargetFile { get; set; }
        public string InputFormat { get; set; }
        public string ReplacePattern { get; set; }
        public bool UseRegex { get; set; }
    }
}
