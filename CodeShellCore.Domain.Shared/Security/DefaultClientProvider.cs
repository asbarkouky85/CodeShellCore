using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security
{
    public class DefaultClientProvider : IClientProvider
    {
        public Task<IEnumerable<AppClient>> Get()
        {
            return Task.Run(() => (IEnumerable<AppClient>)new AppClient[0]);
        }
    }
}
