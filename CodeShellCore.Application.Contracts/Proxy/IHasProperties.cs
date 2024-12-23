using System.Collections.Generic;

namespace CodeShellCore.Proxy
{
    public interface IHasProperties
    {
        Dictionary<string, PropertyDto> Properties { get; set; }
        bool IsDetail { get; set; }
    }
}