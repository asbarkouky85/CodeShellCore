using CodeShellCore.Data;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Resources
{
    public interface IMoldsterResourceRepository : IRepository<Resource>
    {
        Task<Resource> GetResource(string resourceName, string serviceName = null, List<Domain> lst = null);
        Task<IEnumerable<string>> GetByMoldsterModule(string installPath);
    }
}
