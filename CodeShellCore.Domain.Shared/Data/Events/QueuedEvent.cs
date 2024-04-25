using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Events
{
    public class QueuedEvent
    {
        public Type EventType { get; set; }
        public object EventData { get; set; }

        public QueuedEvent(Type eventType, object eventData)
        {
            EventType = eventType;
            EventData = eventData;
        }
    }
}
