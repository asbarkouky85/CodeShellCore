using CodeShellCore.Data;
using CodeShellCore.Moldster.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Data
{
    public interface IConfigUnit : IUnitOfWork
    {
        IRepository<Tenant> TenantRepository { get; }
        IRepository<Control> ControlRepository { get; }
        IRepository<App> AppRepository { get; }
        IRepository<ResourceAction> ResourceActionRepository { get; }
        IRepository<ResourceCollection> ResourceCollectionRepository { get; }

        IDomainRepository DomainRepository { get; }
        ICustomTextRepository CustomTextRepository { get; }
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
