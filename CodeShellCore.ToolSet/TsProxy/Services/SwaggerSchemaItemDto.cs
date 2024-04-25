using System.Collections.Generic;

namespace CodeShellCore.ToolSet.TsProxy.Services
{
    public class SwaggerSchemaItemDto
    {
        public string Type { get; set; }
        public Dictionary<string,SwaggerPropertyDto> Properties { get; set; }
    }
}