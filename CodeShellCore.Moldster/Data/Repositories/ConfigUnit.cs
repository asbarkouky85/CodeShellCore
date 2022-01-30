using CodeShellCore.Data;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Moldster.Data;
using System;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Data.Repositories.Internal;

namespace CodeShellCore.Moldster.Data.Repositories
{
    public class ConfigUnit : UnitOfWork<MoldsterContext>, IConfigUnit
    {
        public ConfigUnit(IServiceProvider provider) : base(provider)
        {
        }


        protected override Type GenericRepositoryType => typeof(MoldsterRepository<,>);
        protected override bool UseChangeColumns => true;

        public ICustomTextRepository CustomTextRepository => GetRepository<ICustomTextRepository>();
        public IDomainRepository DomainRepository { get { return GetRepository<IDomainRepository>(); } }
        public IMoldsterResourceRepository ResourceRepository { get { return GetRepository<IMoldsterResourceRepository>(); } }
        public INavigationGroupRepository NavigationGroupRepository => GetRepository<INavigationGroupRepository>();
        public INavigationPageRepository NavigationPageRepository => GetRepository<INavigationPageRepository>();
        public IPageCategoryParameterRepository PageCategoryParameterRepository => GetRepository<IPageCategoryParameterRepository>();
        public IPageCategoryRepository PageCategoryRepository { get { return GetRepository<IPageCategoryRepository>(); } }
        public IPageControlRepository PageControlRepository { get { return GetRepository<IPageControlRepository>(); } }
        public IPageParameterRepository PageParameterRepository => GetRepository<IPageParameterRepository>();
        public IPageRepository PageRepository { get { return GetRepository<IPageRepository>(); } }
        public IPageRouteRepository PageRouteRepository => GetRepository<IPageRouteRepository>();

        public IRepository<App> AppRepository { get { return GetRepositoryFor<App>(); } }
        public IRepository<Control> ControlRepository { get { return GetRepositoryFor<Control>(); } }
        public IRepository<CustomField> CustomFieldRepository => GetRepositoryFor<CustomField>();
        
        public IRepository<ResourceAction> ResourceActionRepository { get { return GetRepositoryFor<ResourceAction>(); } }
        public IRepository<ResourceCollection> ResourceCollectionRepository => GetRepositoryFor<ResourceCollection>();
        public IRepository<Tenant> TenantRepository { get { return GetRepositoryFor<Tenant>(); } }

        
    }
}
