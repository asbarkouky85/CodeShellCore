using CodeShellCore.Extensions.DependencyInjection;
using CodeShellCore.Modularity;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Resources;
using CodeShellCore.Moldster.Sql;
using CodeShellCore.Moldster.Tenants;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Moldster
{
    [DependsOn(
        typeof(CodeShellEntityFrameworkModule),
        typeof(MoldsterDomainModule)
        )]
    public class MoldsterEntityFrameworkModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddCodeshellDbContext<MoldsterContext>();

            context.Services.AddUnitOfWork<MoldsterUnit, IMoldsterUnit>();
            context.Services.AddGenericRepository(typeof(MoldsterRepository<,>));

            context.Services.AddRepositoryFor<CustomText, CustomTextRepository, ICustomTextRepository>();
            context.Services.AddRepositoryFor<PageControl, PageControlRepository, IPageControlRepository>();
            context.Services.AddRepositoryFor<PageCategory, PageCategoryRepository, IPageCategoryRepository>();
            context.Services.AddRepositoryFor<Domain, DomainRepository, IDomainRepository>();
            context.Services.AddRepositoryFor<Resource, ResourceRepository, IMoldsterResourceRepository>();
            context.Services.AddRepositoryFor<Page, PageRepository, IPageRepository>();
            context.Services.AddRepositoryFor<NavigationGroup, NavigationGroupRepository, INavigationGroupRepository>();
            context.Services.AddRepositoryFor<NavigationPage, NavigationPageRepository, INavigationPageRepository>();
            context.Services.AddRepositoryFor<PageCategoryParameter, PageCategoryParameterRepository, IPageCategoryParameterRepository>();
            context.Services.AddRepositoryFor<PageParameter, PageParameterRepository, IPageParameterRepository>();
            context.Services.AddRepositoryFor<PageRoute, PageRouteRepository, IPageRouteRepository>();
            context.Services.AddRepositoryFor<Tenant, TenantRepository, ITenantRepository>();

            context.Services.AddTransient<ISqlCommandService, SqlCommandService>();
        }
    }
}
