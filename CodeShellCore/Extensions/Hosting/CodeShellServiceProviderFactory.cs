using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.Extensions.Hosting
{
    public class CodeShellServiceProviderFactory : IServiceProviderFactory<IAppBuilder>
    {
        private readonly IAppBuilder builder;

        public CodeShellServiceProviderFactory(IAppBuilder builder)
        {
            this.builder = builder;
        }
        public IAppBuilder CreateBuilder(IServiceCollection services)
        {
            builder.RegisterServices(services);
            return builder;
        }

        public IServiceProvider CreateServiceProvider(IAppBuilder containerBuilder)
        {
            return containerBuilder.BuildServiceProvider();
        }
    }
}
