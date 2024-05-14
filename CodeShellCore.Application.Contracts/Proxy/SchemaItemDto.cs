using System.Collections.Generic;

namespace CodeShellCore.Proxy
{
    public class SchemaItemDto
    {
        public string Name { get; set; }
        public Dictionary<string, PropertyDto> Properties { get; set; } = new Dictionary<string, PropertyDto>();
        public Dictionary<string, long> Items { get; set; }
        public List<string> GenericArguments { get; set; }
    }
}