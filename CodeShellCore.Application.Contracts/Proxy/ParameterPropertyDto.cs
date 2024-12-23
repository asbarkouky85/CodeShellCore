using System.Collections.Generic;

namespace CodeShellCore.Proxy
{
    public class ParameterPropertyDto : PropertyDto, IHasProperties
    {
        public Dictionary<string, PropertyDto> Properties { get; set; } = new Dictionary<string, PropertyDto>();
        public bool IsDetail { get; set; }
    }

}