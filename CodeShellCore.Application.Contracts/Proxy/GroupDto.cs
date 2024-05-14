using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Proxy
{
    public class GroupDto
    {
        public Dictionary<string, ServiceDefinitionDto> Services { get; set; } = new Dictionary<string, ServiceDefinitionDto>();

    }


}
