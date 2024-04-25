using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Linq;

namespace CodeShellCore.Data.Lookups
{
    public class LookupsPagedResultRequestDto : PagedListRequestDto
    {
        public string CollectionId { get; set; }
        public string Entity { get; set; }
    }
}
