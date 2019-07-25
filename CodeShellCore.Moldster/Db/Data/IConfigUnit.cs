using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface IConfigUnit : IUnitOfWork
    {
        IRepository<Tenant> TenantRepository { get; }
        IPageCategoryRepository PageCategoryRepository { get; }
        IPageRepository PageRepository { get; }
        IRepository<DomainEntity> DomainEntityRepository { get; }
        ITenantDomainRepository TenantDomainRepository { get; }
        IRepository<Domain> DomainRepository { get; }
        IRepository<DomainEntityProperty> EntityPropertyRepository { get; }
        IRepository<Control> ControlRepository { get; }
        IRepository<TenantApp> TenantAppRepository { get; }
        IRepository<ResourceAction> ResourceActionRepository { get; }
        IMoldsterResourceRepository ResourceRepository { get; }
        IPageControlRepository PageControlRepository { get; }
    }
}
