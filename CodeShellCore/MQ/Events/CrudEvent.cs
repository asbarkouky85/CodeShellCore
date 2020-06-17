using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MQ.Events
{
    public abstract class CrudEventBase
    {
        public long? TenantId { get; set; }
        public ActionType Type { get; protected set; }
    }
}
