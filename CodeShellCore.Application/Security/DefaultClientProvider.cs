using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security
{
    public class DefaultClientProvider : IClientProvider
    {
        public IEnumerable<AppClient> Get()
        {
            return new AppClient[0];
        }
    }
}
