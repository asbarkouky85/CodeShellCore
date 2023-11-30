using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public interface IAddDistributedEvent
    {
        void AddDistributedEvent(object eventData, Type type = null);
    }
}
