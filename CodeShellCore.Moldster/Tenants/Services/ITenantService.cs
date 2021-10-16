using CodeShellCore.Data.Helpers;
using CodeShellCore.Moldster;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Data
{
    public interface ITenantService : IServiceBase
    {
        SubmitResult Create(Tenant model);
    }
}
