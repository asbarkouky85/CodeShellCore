using System.Collections.Generic;

namespace CodeShellCore.Proxy
{
    public class PropertyDto
    {
        public string Namespace { get; set; }
        public string SchemaName { get; set; }
        public string Type { get; set; }
        public bool Nullable { get; set; }
        public List<PropertyDto> GenericArguments { get; set; }
        
    }

}