using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Dto
{
    public class RenderDTO
    {
        public string Mod { get; set; }
        public string Domain { get; set; }
        public bool? Lazy { get; set; }

        public string NameChain { get; set; }
        public long? TenantId { get; set; }
        public bool? Recursive { get; set; } = true;
    }
}
