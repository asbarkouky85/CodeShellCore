using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.Cli
{
    public interface ICodeShellContainerBuilder
    {
        void RegisterServices(IServiceCollection collection);
        IServiceProvider BuildServiceProvider();
    }
}
