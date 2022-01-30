using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MQ.Events
{
    public class EventFactory
    {
        private static CrudEventBase GetEvent(object ob, ActionType t, long? tenantId = null)
        {
            Type x = typeof(CrudEvent<>);
            Type x2 = x.MakeGenericType(ob.GetType());
            return (CrudEventBase)Activator.CreateInstance(x2, ob, t, tenantId);
        }
        public static CrudEventBase GetUpdatedEvent(ISharedModel ob, long tenantId)
        {
            return GetEvent(ob.Copy(), ActionType.Update, tenantId);
        }

        public static CrudEventBase GetAddedEvent(ISharedModel ob, long tenantId)
        {
            return GetEvent(ob.Copy(), ActionType.Add, tenantId);
        }

        public static CrudEventBase GetDeletedEvent(ISharedModel ob, long tenantId)
        {
            return GetEvent(ob.Copy(), ActionType.Delete, tenantId);
        }
    }
}
