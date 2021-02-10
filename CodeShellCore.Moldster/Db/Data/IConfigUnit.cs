using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface IConfigUnit : IUnitOfWork
    {
        IRepository<Tenant> TenantRepository { get; }
        IRepository<DomainEntity> DomainEntityRepository { get; }
        IRepository<DomainEntityProperty> EntityPropertyRepository { get; }
        IRepository<Control> ControlRepository { get; }
        IRepository<TenantApp> TenantAppRepository { get; }
        IRepository<ResourceAction> ResourceActionRepository { get; }
        IRepository<DomainEntityCollection> DomainEntityCollectionRepository { get; }

        IDomainRepository DomainRepository { get; }
        IMoldsterResourceRepository ResourceRepository { get; }
        INavigationGroupRepository NavigationGroupRepository { get; }
        INavigationPageRepository NavigationPageRepository { get; }
        IPageCategoryParameterRepository PageCategoryParameterRepository { get; }
        IPageCategoryRepository PageCategoryRepository { get; }
        IPageControlRepository PageControlRepository { get; }
        IPageRepository PageRepository { get; }
        IPageParameterRepository PageParameterRepository { get; }
        IPageRouteRepository PageRouteRepository { get; }
        IRepository<CustomField> CustomFieldRepository { get; }
    }
}
