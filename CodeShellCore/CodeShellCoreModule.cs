using CodeShellCore.Caching;
using CodeShellCore.Cli;
using CodeShellCore.Files.Logging;
using CodeShellCore.Helpers;
using CodeShellCore.Modularity;
using CodeShellCore.MQ;
using CodeShellCore.MQ.Internal;
using CodeShellCore.MultiTenant;
using CodeShellCore.Security;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Tasks;
using CodeShellCore.Text.Localization;
using CodeShellCore.Text.TextProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CodeShellCore
{
    public class CodeShellCoreModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddOptions<CodeShellAppOptions>();
            context.Services.Configure<CodeShellAppOptions>(context.Configuration.GetSection(ConfigNames.CodeShellApp));

            context.Services.AddSingleton<JobConfig>();

            context.Services.AddScoped<ClientData>();
            context.Services.AddScoped<CurrentTenant>();
            context.Services.AddScoped<IUserAccessor, UserAccessor>();
            context.Services.AddScoped<Language>();
            context.Services.AddScoped<UserAccessor>();

            context.Services.AddTransient<ILocaleTextProvider, ResxTextProvider>();
            
            context.Services.AddTransient<ITenantDataProvider, NullTenantDataProvider>();
            context.Services.AddTransient<IUserDataService, NullUserDataService>();
            context.Services.AddTransient<IJobRunner, JobRunner>();
            context.Services.AddTransient<ICacheProvider, MemoryCacheProvider>();

            var bus = new DefaultServiceBus(e => { });
            context.Services.AddSingleton<IServiceBus>(bus);

            context.Services.AddLogging();
        }

        public override void OnApplicationStarted(CodeShellApplicationInitializationContext context)
        {
            var opts = context.ServiceProvider.GetRequiredService<IOptions<CodeShellAppOptions>>();
            if (opts.Value.UseJobs)
            {
                var jobs = context.ServiceProvider.GetService<JobConfig>().Jobs;
                foreach (var job in jobs)
                {
                    IJobRunner runner = context.ServiceProvider.GetService<IJobRunner>();
                    runner.Job = job;
                    runner.Timer.Start();
                    if (job.RunOnStartUp)
                    {
                        _ = runner.RunJob();
                    }
                }
            }

            if (opts.Value.UseTransporter)
                Transporter.Start();
        }

        public override void OnApplicationStopped()
        {
            Logger.Default?.Dispose();
            Transporter.Exit();
        }
    }
}
