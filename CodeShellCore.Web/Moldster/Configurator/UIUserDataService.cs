using CodeShellCore.Caching;
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
        public UIUserDataService(ICacheProvider cache) : base(cache)
        {
        }

        protected override IUser GetUserFromDataSource(object c)
        {
            return new ConfiguratorUserDTO();
        }
    }
}
