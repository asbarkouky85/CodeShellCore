using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Tenants.Services
{
    public interface ITenantService : IServiceBase
    {
        SubmitResult Create(Tenant model);
    }
}
