using CodeShellCore.Cli;
using CodeShellCore.Modularity;
using Microsoft.Extensions.Configuration;
using System;

namespace CodeShellCore.Extensions.Hosting
{
    public interface IAppBuilder : ICodeShellContainerBuilder
    {
        IAppBuilder UseModule<T>(Func<IConfigurationRoot> configBuilder = null) where T : CodeShellModule;

    }
}
