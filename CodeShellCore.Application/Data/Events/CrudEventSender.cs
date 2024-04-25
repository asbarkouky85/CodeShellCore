using CodeShellCore.Data.Mapping;
using CodeShellCore.Files.Logging;
using CodeShellCore.MQ;
using CodeShellCore.MQ.Events;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Events
{
    public class CrudEventSender : ICrudEventSender
    {
        CrudEventSenderOptions Options;
        IObjectMapper _mapper;
        public CrudEventSender(IOptions<CrudEventSenderOptions> options, IObjectMapper mapper)
        {
            Options = options.Value;
            _mapper = mapper;
        }

        public bool IsEnabled => Options.Enable;

        public Task PublishEvent<T>(T entity, ActionType changeType, long? tenantId = null)
        {

            return PublishEvent(typeof(T), entity, changeType, tenantId);
        }

        public Task PublishEvent(Type type, object entity, ActionType changeType, long? tenantId = null)
        {
            if (!Options.Enable)
                return Task.CompletedTask;

            return Task.Run(async () =>
            {
                if (Options.Entities.TryGetEvent(type, out Type eventType))
                {
                    try
                    {
                        var data = _mapper.Map(entity, type, eventType);
                        Type crudEventType = typeof(CrudEvent<>).MakeGenericType(eventType);
                        var evnt = (CrudEventBase)Activator.CreateInstance(crudEventType, data, changeType, tenantId);
                        await Transporter.PublishAsync(evnt, crudEventType);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteException(ex);
                    }

                }
            });
        }
    }
}
