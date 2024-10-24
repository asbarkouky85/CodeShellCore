using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security
{
    public interface IClientProvider
    {
        IEnumerable<AppClient> Get();
    }
}
