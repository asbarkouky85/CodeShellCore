using System;
using System.Threading.Tasks;
using MassTransit;
using CodeShellCore.Data.Helpers;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Services;
using CodeShellCore.Files.Logging;
using CodeShellCore.Types;
using CodeShellCore.Files.Storage;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Data.Events;
using CodeShellCore.MultiTenant;
using System.Collections.Generic;
using CodeShellCore.Data.Mapping;

namespace CodeShellCore.MQ
{

    public abstract class Consumer : ServiceBase, IConsumer
    {
        private IServiceScope _scope;
        private static Logger _logger;
        private static object _locker = new { };
        protected Logger MQLog => _getLogger();
        protected InstanceStore<object> Store { get; private set; }
        protected IServiceProvider Injector { get; private set; }
        protected IObjectMapper Mapper => Store.GetRequiredService<IObjectMapper>();
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

        protected async Task ConsumeEvent<T>(T ev, Func<T, Task<SubmitResult>> action, long? tenantId = null) where T : class
        {
            try
            {
                if (tenantId != null)
                    Injector.SetCurrentTenant(tenantId.Value);
                var res = await action(ev);
                RecordResult(res, ev);
            }
            catch (Exception ex)
            {
                SubmitResult re = new SubmitResult(1);
                re.SetException(ex);
                RecordResult(re, ev);
                Logger.WriteException(ex);
                throw;
            }
        }

        //protected async Task ConsumeEvent<T>(ConsumeContext<T> context, Func<T, Task<SubmitResult>> action, long? tenantId = null) where T : class
        //{
        //    try
        //    {
        //        if (tenantId != null)
        //            Injector.SetCurrentTenant(tenantId.Value);
        //        var res = await action(context.Message);
        //        RecordResult(res, context.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        SubmitResult re = new SubmitResult(1);
        //        re.SetException(ex);
        //        RecordResult(re, context.Message);
        //        throw;
        //    }
        //}

        public Task<Dictionary<long, SubmitResult>> ConsumeForAllTenants<T>(T ev, Func<T, Task<SubmitResult>> action) where T : class
        {
            return Task.Run(async () =>
            {
                var provider = Injector.GetService<ITenantDataProvider>();
                Dictionary<long, string> dic = provider.GetContectionStringDictionary();
                Dictionary<long, SubmitResult> ret = new Dictionary<long, SubmitResult>();
                foreach (var ob in dic)
                {
                    using (var sc = Shell.GetScope())
                    {
                        try
                        {
                            sc.ServiceProvider.SetCurrentTenant(ob.Key);
                            ret[ob.Key] = await action(ev);
                        }
                        catch (Exception ex)
                        {
                            SubmitResult res = new SubmitResult();
                            res.SetException(ex);
                            res.Code = 1;
                            ret[ob.Key] = res;
                        }
                        RecordResult(ret[ob.Key], ev);
                    }
                }
                return ret;
            });

        }

        protected async Task Respond<T, TR>(ConsumeContext<T> context, Func<T, Task<TR>> action) where T : class where TR : class
        {
            try
            {
                var res = await action(context.Message);
                await context.RespondAsync<TR>(res);
            }
            catch (Exception ex)
            {
                SubmitResult re = new SubmitResult(1);
                re.SetException(ex);
                RecordResult(re, context.Message);
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
