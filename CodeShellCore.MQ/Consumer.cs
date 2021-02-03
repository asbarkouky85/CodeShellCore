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
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.MQ
{

    public abstract class Consumer : ServiceBase, IConsumer
    {
        protected IServiceScope _scope;
        protected InstanceStore<object> Store;
        private static Logger _logger;
        private static object _locker = new { };
        protected Logger MQLog => _getLogger();
        protected IServiceProvider Injector { get; private set; }
        public Consumer()
        {
            _scope = Shell.GetScope();
            Injector = _scope.ServiceProvider;
            Store = new InstanceStore<object>(() => Injector);
        }


        private Logger _getLogger()
        {
            if (_logger == null)
            {
                lock (_locker)
                {
                    _logger = Logger.Create(Shell.ProjectAssembly.GetName().Name, Path.Combine(Shell.AppRootPath, "MQLogs"));
                }
            }
            _logger.ClassName = GetType().Name;
            return _logger;
        }

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

        protected Task ConsumeEvent<T>(T ev, Func<T, SubmitResult> action) where T : class
        {
            return Task.Run(() =>
            {
                try
                {

                    var res = action(ev);
                    RecordResult(res, ev);
                }
                catch (Exception ex)
                {
                    SubmitResult re = new SubmitResult(1);
                    re.SetException(ex);
                    RecordResult(re, ev);
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
                    RecordResult(res, context.Message);
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

        protected Task Respond<T, TR>(ConsumeContext<T> context, Func<T, TR> action) where T : class where TR : class
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

        protected void RecordResult(SubmitResult res, object data)
        {
            if (res.IsSuccess)
            {
                MQLog.WriteLogLine($"Event {data.GetType().FullName} ---> Success [{res.AffectedRows}] rows");
            }
            else
            {
                MQLog.WriteLogLine($"Event {data.GetType().FullName} ---> Failed {res.Message} {res.ExceptionMessage}");
                var ex = res.GetException();
                if (ex != null)
                {
                    var trace = ex.GetStackTrace(true);
                    foreach (var line in trace)
                    {
                        MQLog.WriteLogLine(line);
                    }

                }
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            _scope.Dispose();
        }


    }
}
