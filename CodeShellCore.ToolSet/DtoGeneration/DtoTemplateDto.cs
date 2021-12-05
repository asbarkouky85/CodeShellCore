using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.ToolSet.DtoGeneration
{
    public class DtoTemplateDto
    {
        public string Namespace { get; set; }
        public string DtoClassName { get; set; }
        public string EntityClassName { get; set; }
        public string Properties { get; set; }
    }
}
