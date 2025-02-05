using System.Collections.Generic;

namespace CodeShellCore.Proxy
{
    public class ProxyModelsFile
    {
        public string Namespace { get; set; }
        public Dictionary<string, PropertyDto> Importations { get; set; } = new Dictionary<string, PropertyDto>();
        public List<string> Entities { get; set; } = new List<string>();
    }

    public class ProxyServiceFile
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public Dictionary<string, PropertyDto> Importations { get; set; } = new Dictionary<string, PropertyDto>();
        public string Content { get; set; }
    }
}
