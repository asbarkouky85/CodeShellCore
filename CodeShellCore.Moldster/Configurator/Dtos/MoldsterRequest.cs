using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Configurator
{
    public class MoldsterRequest
    {
        public long? PageCategoryId { get; set; }
        public long? PageId { get; set; }
        public long? DomainId { get; set; }
        public long TenantId { get; set; }
    }
}
