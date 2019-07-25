using System;
using CodeShellCore.Data;
using CodeShellCore.Data.EntityFramework;

namespace CodeShellCore.Moldster.Db.Data.Internal
{
    public class TenantDomainRepository : MoldsterRepository<TenantDomain, ConfigurationContext>,ITenantDomainRepository
    {
        private IRepository<Tenant> Tenants;
        public TenantDomainRepository(ConfigurationContext con, Repository<Tenant,ConfigurationContext> ten) : base(con)
        {
            Tenants = ten;
        }

        public TenantDomain GetTenantDomain(long domainId, string tenantCode)
        {
            long tenantId = Tenants.GetSingleValue(e => e.Id, d => d.Code == tenantCode);
            if (tenantId == 0)
                throw new Exception("Unregistered tenant " + tenantCode);

            TenantDomain dom = FindSingle(d => d.TenantId == tenantId && d.DomainId == domainId);

            if (dom == null)
            {
                dom = new TenantDomain
                {
                    Id = long.Parse(domainId.ToString() + tenantId.ToString()+"0"),
                    TenantId = tenantId,
                    DomainId = domainId
                };
                Add(dom);
            }
            return dom;
        }
    }
}
