using CodeShellCore.Data;
using CodeShellCore.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface IMoldsterResourceRepository : IRepository<Resource>
    {
        Resource GetResource(long domainId, string resourceName);
    }
}
