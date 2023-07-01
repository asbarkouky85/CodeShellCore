using CodeShellCore.Data;
using CodeShellCore.Moldster.Data.Repositories;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Resources;
using CodeShellCore.Moldster.Tenants;

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
