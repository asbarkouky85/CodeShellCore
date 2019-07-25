using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Web
{
    public class ShellStartup<T> where T : WebShell
    {

        private WebShell _ProjectShell;

        public ShellStartup(IConfiguration configuration)
        {
            Configuration = configuration;
            _ProjectShell = (T)Activator.CreateInstance(typeof(T), configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _ProjectShell.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            _ProjectShell.ConfigureHttp(app, env);
            
            Shell.Start(_ProjectShell);

            var l=app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            l.ApplicationStopped.Register(OnShutdown);
        }

        private void OnShutdown()
        {
            Shell.Exit();
        }
    }
}