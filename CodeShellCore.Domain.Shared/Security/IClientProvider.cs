using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security
{
    public interface IClientProvider
    {
        Task<IEnumerable<AppClient>> Get();
    }
}
