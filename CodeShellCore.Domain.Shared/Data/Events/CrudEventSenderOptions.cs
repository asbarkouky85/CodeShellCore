using System;
using System.Collections.Generic;

namespace CodeShellCore.Data.Events
{
    public class CrudEventSenderOptions
    {
        public bool Enable { get; set; } = false;
        public CrudEventSenderEntities Entities { get; private set; } = new CrudEventSenderEntities();
    }
}