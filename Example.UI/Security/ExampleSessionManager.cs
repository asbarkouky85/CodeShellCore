using CodeShellCore.Security.Authorization;
using CodeShellCore.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.UI.Security
{
    public class ExampleSessionManager : TokenSessionManager
    {
        public ExampleSessionManager(IServiceProvider prov) : base(prov)
        {
        }

        protected override AppClient[] Clients => new[] { new AppClient { ClientId = "Test.App" } };
    }
}
