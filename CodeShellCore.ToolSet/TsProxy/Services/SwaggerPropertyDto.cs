using System.Collections.Generic;

namespace CodeShellCore.ToolSet.TsProxy.Services
{
    public class SwaggerPropertyDto
    {
        public string Type { get; set; }
        public bool Nullable { get; set; }
        public Dictionary<string, string> Items { get; set; }
    }
}