using CodeShellCore.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Moldster.Configurator
{
    public class ConfiguratorUserDTO : IUser
    {
        public object UserId => 1;

        public string Name => "Administrator";

        public string LogonName => "admin";
    }
}
