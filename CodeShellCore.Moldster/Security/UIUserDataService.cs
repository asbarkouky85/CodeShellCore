using CodeShellCore.Caching;
using CodeShellCore.Moldster.Security;
using CodeShellCore.MultiTenant;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Moldster.Configurator
{
    public class UIUserDataService : UserDataService
    {
        public UIUserDataService(ICacheProvider cache, CurrentTenant tenant) : base(cache, tenant)
        {
        }

        protected override Task<IUser> GetUserFromDataSource(string c)
        {
            return Task.FromResult((IUser)new ConfiguratorUserDTO());
        }
    }
}
