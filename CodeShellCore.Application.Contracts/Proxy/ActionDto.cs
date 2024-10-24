using System.Collections.Generic;

namespace CodeShellCore.Proxy
{
    public class ActionDto
    {
        public string Path { get; set; }
        public string Method { get; set; }
        public Dictionary<string, PropertyDto> RouteParameters { get; set; } = new Dictionary<string, PropertyDto>();
        public Dictionary<string, ParameterPropertyDto> Parameters { get; set; } = new Dictionary<string, ParameterPropertyDto>();
        public PropertyDto RequestBody { get; set; }
        public Dictionary<string, ResponsePropertyDto> Responses { get; set; } = new Dictionary<string, ResponsePropertyDto>();
    }

}