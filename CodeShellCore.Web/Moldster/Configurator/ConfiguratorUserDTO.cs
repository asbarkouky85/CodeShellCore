using CodeShellCore.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Moldster.Configurator
{
    public class ConfiguratorUserDTO : IUser
    {
        public string UserId => "1";

        public string Name => "Administrator";

        public string LogonName => "admin";

        public long? GetUserIdAsLong()
        {
            return null;
        }
    }
}
