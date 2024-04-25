using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.ToolSet.TsProxy.Services
{
    public class SwaggerDocumentDto
    {
        public Dictionary<string,SwaggerPathDto> Paths { get; set; }
        public SwaggerComponentsDto Components { get; set; }
    }
}
