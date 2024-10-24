using CodeShellCore.Modularity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CodeShellCore.Web
{
    public class WebModuleStartup<TModule> : CodeShellContainerBuilder<TModule> where TModule : CodeShellModule
    {
        public WebModuleStartup(IConfiguration configuration) : base(configuration, null)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var context = new CodeshellAppContext(services, Configuration);
            RegisterModule(typeof(TModule), context);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Shell.SetRootPath(env.ContentRootPath);
            SetRootProvider(app.ApplicationServices);

            var context = new CodeShellApplicationInitializationContext(app.ApplicationServices, Configuration);
            context.AddItem(app);
            context.AddItem(env);
            ConfigureModule(typeof(TModule), context);

            var hostApplicationLifeTime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            hostApplicationLifeTime.ApplicationStarted.Register(() => StartModule(typeof(TModule)));
            hostApplicationLifeTime.ApplicationStopped.Register(() => StopModule(typeof(TModule)));
        }
    }
}
