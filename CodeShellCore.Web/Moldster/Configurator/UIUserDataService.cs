using CodeShellCore.Caching;
using CodeShellCore.MultiTenant;
using CodeShellCore.Security;
using CodeShellCore.Security.Authorization;
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

        protected override IUser GetUserFromDataSource(string c)
        {
            return new ConfiguratorUserDTO();
        }
    }
}
