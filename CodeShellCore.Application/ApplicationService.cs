
using CodeShellCore.Data.Mapping;
using CodeShellCore.Security;
using CodeShellCore.Text.Localization;
using CodeShellCore.Types;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.Services
{
    public class ApplicationService : ServiceBase, IDisposable
    {
        protected IObjectMapper Mapper { get; private set; }
        protected InstanceStore Store { get; private set; }
        protected IUserAccessor UserAccessor => Store.GetRequiredService<IUserAccessor>();
        protected Language Language => Store.GetRequiredService<Language>();
        public ApplicationService(IServiceProvider provider)
        {
            Store = new InstanceStore(provider);
            Mapper = provider.GetRequiredService<IObjectMapper>();
        }

        protected T GetService<T>()
        {
            return Store.GetRequiredService<T>();
        }

        public override void Dispose()
        {
            base.Dispose();
            Store.Clear();
        }
    }
}
