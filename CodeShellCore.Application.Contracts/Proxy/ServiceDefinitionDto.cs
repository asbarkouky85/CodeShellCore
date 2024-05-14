using System.Collections.Generic;

namespace CodeShellCore.Proxy
{
    public class ServiceDefinitionDto
    {
        public Dictionary<string, ActionDto> Actions { get; set; } = new Dictionary<string, ActionDto>();
        public string Namespace { get;set; }
    }
}
