using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CodeShellCore.Moldster.Pages.Dtos
{
    public class FindPageRequest
    {
        public long TenantId { get; set; }
        public int Type { get; set; }
        [IgnoreDataMember]
        public PageTypes TypeEnum { get { return (PageTypes)Type; } }
    }
}
