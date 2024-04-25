using CodeShellCore.MQ.Events;
using System;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Events
{
    public interface ICrudEventSender
    {
        bool IsEnabled { get; }
        Task PublishEvent(Type type, object entity, ActionType changeType, long? tenantId = null);
        Task PublishEvent<T>(T entity, ActionType changeType, long? tenantId = null);
    }
}