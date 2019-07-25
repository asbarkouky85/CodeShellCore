using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Moldster.Db.Data;

namespace CodeShellCore.Moldster.Db.Services
{
    public class TenantsService : EntityService<Tenant>
    {
        public TenantsService(IConfigUnit unit) : base(unit)
        {
            Unit = unit;
        }

        IConfigUnit Unit;
        public SubmitResult AddDomain(TenantDomain tenantDomain)
        {
            Unit.TenantDomainRepository.Merge(tenantDomain);
            return Unit.SaveChanges();
        }

        
    }
}
