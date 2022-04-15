using CodeShellCore.Data;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Resources
{
    public interface IMoldsterResourceRepository : IRepository<Resource>
    {
        Resource GetResource(string resourceName, string serviceName = null, List<Domain> lst = null);
        IEnumerable<string> GetByMoldsterModule(string installPath);
    }
}
