using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MQ.Events
{
    public class TenancyEvent<T>
    {
        public long? TenantId { get; set; }
        public T Data { get; set; }


    }
}
