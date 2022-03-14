using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Tenants
{
    public interface ITenantService : IDtoEntityService<long, TenantDto, LoadOptions,TenantEditDTO,TenantDto>
    {
        //SubmitResult Create(Tenant model);
    }
}
