using CodeShellCore.Data.Helpers;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services
{
    public interface ITenantService : IServiceBase
    {
        SubmitResult Create(Tenant model);
    }
}
