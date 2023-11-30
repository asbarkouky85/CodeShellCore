using System;
using System.Collections.Generic;

namespace CodeShellCore.Data.Events
{
    public class CrudEventSenderEntities
    {
        private Dictionary<Type, Type> _entityToEvents = new Dictionary<Type, Type>();

        public void Add<TEntity, TEvent>()
        {
            _entityToEvents[typeof(TEntity)] = typeof(TEvent);
        }

        public bool TryGetEvent(Type entityType, out Type eventType)
        {
            return _entityToEvents.TryGetValue(entityType, out eventType);
        }
    }
}