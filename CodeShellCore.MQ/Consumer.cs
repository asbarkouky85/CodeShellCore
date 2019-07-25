using System;
using System.Threading.Tasks;
using MassTransit;
using CodeShellCore.Data.Helpers;
using CodeShellCore.DependencyInjection;
using CodeShellCore.MQ.Events;
using CodeShellCore.Services;
using CodeShellCore.Files.Logging;
using CodeShellCore.Types;
using CodeShellCore.Files.Storage;

namespace CodeShellCore.MQ
{

    public abstract class Consumer : ServiceBase, IConsumer, IEventHandler
    {
        protected InstanceStore<IServiceBase> Store;
        protected FileStorageService Failed;
        protected FileStorageService Successful;
        public Consumer()
        {
            Logger.WriteLine(GetType().FullName);
            Scope = new ScopeContainer(Shell.GetScope());
            Injector = Scope.Scope.ServiceProvider;
            Store = new InstanceStore<IServiceBase>(() => Injector);
            Failed = new FileStorageService("FailedCommands/" + GetType().Name + ".json");
        }
        protected ScopeContainer Scope;
        protected IServiceProvider Injector { get; private set; }

        public abstract Task Handle<TObj>(TObj item) where TObj : class;

        public virtual Task Handle<TObject>(CrudEvent<TObject> item) where TObject : class
        {
            return Task.Run(() =>
            {
                try
                {
                    var res = Store.GetInstance<IEntityHandler<TObject>>().Handle(item);
                    RecordResult(res, item);
                }
                catch (Exception ex)
                {
                    SubmitResult re = new SubmitResult(1);
                    re.SetException(ex);
                    RecordResult(re, item);
                    Logger.WriteException(ex);
                    throw;
                }
            });
        }

        protected Task ConsumeEvent<T>(ConsumeContext<T> context, Func<T, SubmitResult> action) where T : class
        {
            return Task.Run(() =>
            {
                try
                {
                    var res = action(context.Message);
                    RecordResult(res);
                }
                catch (Exception ex)
                {
                    SubmitResult re = new SubmitResult(1);
                    re.SetException(ex);
                    RecordResult(re, context.Message);
                    throw;
                }
            });
        }

        protected Task Respond<T,TR>(ConsumeContext<T> context, Func<T, TR> action) where T : class where  TR :class 
        {
            
                try
                {
                    var res = action(context.Message);
                    return context.RespondAsync<TR>(res);
                }
                catch (Exception ex)
                {
                    return Task.Run(() =>
                    {
                        SubmitResult re = new SubmitResult(1);
                        re.SetException(ex);
                        RecordResult(re, context.Message);
                        
                    });
                    throw;
            }
            
        }

        protected void RecordResult(SubmitResult res, object data = null)
        {
            if (res.Code != 0)
            {
                Failed.Add(data, res.GetException());
                Failed.Save();
            }
        }

        protected SubmitResult OnError(object ev, Exception ex)
        {
            Failed.Add(ev, ex);
            Failed.Save();
            SubmitResult re = new SubmitResult(1);
            re.SetException(ex);
            throw ex;
        }

        public override void Dispose()
        {
            base.Dispose();
            Scope.Dispose();
        }


    }
}
