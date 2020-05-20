using CodeShellCore.Files.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.MQ
{
    public abstract class EventBusBase : IServiceBus
    {
        public virtual void Exit()
        {
        }

        public abstract Task PublisAsync(object ob, Type t = null, CancellationToken? token = null);

        public virtual void Publish(object ob, Type t = null)
        {
            try
            {
                t = t ?? ob.GetType();
                Task tsk = PublisAsync(ob, t, CancellationToken.None);
                if (tsk != null)
                    Task.WaitAll(tsk);
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Event sending failed ");
                Logger.WriteException(ex);
            }
        }

        public virtual void Start()
        {

        }
    }
}
