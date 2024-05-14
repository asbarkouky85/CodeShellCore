using System.Collections.Generic;

namespace CodeShellCore.Proxy
{
    public class DocumentDto
    {
        public Dictionary<string, GroupDto> Modules { get; set; } = new Dictionary<string, GroupDto>();
        public Dictionary<string, SchemaItemDto> Schemas { get; set; } = new Dictionary<string, SchemaItemDto>();
    }
}
