using AutoMapper;
using CodeShellCore.Data;
using CodeShellCore.Files;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Pages.Views;
using CodeShellCore.Moldster.Resources;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Text;
using System;
using System.Linq;

namespace CodeShellCore.Moldster
{
    public class MoldsterMappingProfile : Profile
    {
        public MoldsterMappingProfile()
        {
            DomainsMapping();
            LocalizationMapping();
            NavigationMapping();
            PageCategoriesMapping();
            PagesMapping();
            ResourcesMapping();
            TenantsMapping();
        }

        void NavigationMapping()
        {
            CreateMap<NavigationGroup, NavigationGroupDTO>();

            CreateMap<NavigationPage, NavigationPageDTO>()
                .ForMember(e => e.ActionName, d => d.MapFrom(e => e.Page.SpecialPermission != null ? e.Page.SpecialPermission : (e.Page.ResourceActionId != null ? e.Page.ResourceAction.Name : null)))
                .ForMember(e => e.PrivilegeType, d => d.MapFrom(e => e.Page.PrivilegeType))
                .ForMember(e => e.Apps, d => d.MapFrom(e => e.Page.Apps))
                .ForMember(e => e.PageIdentifier, d => d.MapFrom(e => e.Page.Domain.Name + "__" + e.Page.Name))
                .ForMember(e => e.ResourceName, d => d.MapFrom(e => e.Page.ResourceId == null ? null : e.Page.Resource.Name))
                .ForMember(e => e.RouteParameters, d => d.MapFrom(e => e.Page.RouteParameters))
                .ForMember(e => e.Url, d => d.MapFrom(e => e.Page.ViewPath));

            CreateMap<NavigationPage, NavigationPageListDTO>()

                .ForMember(e => e.Name, e => e.MapFrom(d => d.Page.Name))
                .ForMember(e => e.Url, e => e.MapFrom(d => d.Page.ViewPath))
                .ForMember(e => e.TenantId, e => e.MapFrom(d => d.Page.TenantId));
        }

        void PageCategoriesMapping()
        {
            CreateMap<PageCategory, PageCategoryDto>();
            CreateMap<PageCategoryDto, PageCategory>()
                .IgnoreId()
                .ForMember(e => e.Controls, d => d.Ignore())
                .ForMember(e => e.PageCategoryParameters, d => d.Ignore());

            CreateMap<PageCategory, PageCategoryListDTO>();
            CreateMap<Control, ControlDto>();

            CreateMap<PageCategoryParameter, PageCategoryParameterDto>()
                .MapChangeState();

            CreateMap<PageCategoryParameterDto, PageCategoryParameter>()
                .IgnoreId();

            CreateMap<PageCategory, ModuleCategoryDTO>()
                .ForMember(e => e.Base, e => e.MapFrom(c => c.BaseComponent))
                .ForMember(e => e.Path, e => e.MapFrom(c => c.ViewPath))
                .ForMember(e => e.Resource, e => e.MapFrom(c => c.ResourceId == null ? null : c.Resource.Name));

            CreateMap<PageCategory, PageCategoryEditDto>()
                .ForMember(e => e.Category, d => d.MapFrom(e => e))
                .ForMember(e => e.Resource, d => d.MapFrom(e => e.Resource == null ? null : e.Resource.Name))
                .ForMember(e => e.Domain, d => d.MapFrom(e => e.Resource == null ? null : e.Domain.Name))
                .ForMember(e => e.ResourceDomain, d => d.MapFrom(e => e.Resource == null ? null : e.Resource.Domain == null ? null : e.Resource.Domain.NameChain));
        }

        void ResourcesMapping()
        {
            CreateMap<Resource, ResourceListDTO>()
                .ForMember(e => e.Domain, d => d.MapFrom(e => e.Domain.Name));
        }

        void DomainsMapping()
        {
            CreateMap<Domain, DomainDto>()
                .ForMember(e => e.DomainName, d => d.MapFrom(e => e.Name));

            CreateMap<Domain, DomainWithPagesDTO>()
                .ForMember(e => e.DomainName, e => e.MapFrom(d => d.Name))
                .ForMember(e => e.Pages, d => d.MapFrom(e => e.Pages));

        }

        void LocalizationMapping()
        {

        }

        void PagesMapping()
        {
            CreateMap<Page, PageDto>();

            CreateMap<Page, PageListDTO>()
                .ForMember(e => e.ActionType, d => d.MapFrom(e => e.ResourceActionId.HasValue ? e.ResourceAction.Name : e.SpecialPermission == null ? e.PrivilegeType : e.SpecialPermission))
                .ForMember(e => e.PageType, d => d.MapFrom(e => e.HasRoute && e.CanEmbed ? "Route,Embedded" : e.HasRoute ? "Route" : "Embedded"))
                .ForMember(e => e.BaseComponent, d => d.MapFrom(e => e.PageCategory.BaseComponent));

            CreateMap<Page, CreatePageDTO>()
                .ForMember(e => e.ActionType, e => e.MapFrom(p => p.PrivilegeType))
                .ForMember(e => e.CategoryId, e => e.MapFrom(p => p.PageCategoryId))
                .ForMember(e => e.CollectionId, e => e.MapFrom(p => p.SourceCollectionId))
                .ForMember(e => e.ComponentName, e => e.MapFrom(a => a.ViewPath.GetAfterLast("/")))
                .ForMember(e => e.CollectionId, e => e.MapFrom(p => p.SourceCollectionId))
                .ForMember(e => e.CollectionId, e => e.MapFrom(p => p.SourceCollectionId))
                .ForMember(e => e.CollectionId, e => e.MapFrom(p => p.SourceCollectionId))
                .ForMember(e => e.AppsString, e => e.MapFrom(p => p.Apps))
                .ForMember(e => e.Apps, e => e.MapFrom(p => p.GetAppsList()));

            CreateMap<Page, PageDetailsDto>()
                .ForMember(e => e.ParentHasResource, e => e.MapFrom(p => p.PageCategory.ResourceId != null))
                .ForMember(e => e.BaseViewPath, e => e.MapFrom(p => p.PageCategory.ViewPath))
                .ForMember(e => e.ActionName, e => e.MapFrom(p => p.ResourceAction == null ? (p.SpecialPermission ?? null) : p.ResourceAction.Name))
                .ForMember(e => e.PageIdentifier, e => e.MapFrom(p => p.Domain.Name + "__" + p.Name))
                .ForMember(e => e.ComponentName, d => d.MapFrom(e => e.SourceCollection == null ? null : e.SourceCollection.Name))
                .ForMember(e => e.Page, d => d.MapFrom(e => e));

            CreateMap<PageControl, PageControlListDTO>()
                .ForMember(e => e.ControlType, d => d.MapFrom(e => e.Control.ControlType))
                .MapChangeState();

            CreateMap<PageControl, ControlRenderDto>()
                .ForMember(e => e.Identifier, e => e.MapFrom(d => d.Control.Identifier))
                .ForMember(e => e.Accessibilty, e => e.MapFrom(d => d.Accessability))
                .ForMember(e => e.Collection, e => e.MapFrom(
                    d => d.SourceCollection != null ? new CollectionDTO
                    {
                        Id = d.SourceCollection.Id,
                        Name = d.SourceCollection.Name
                    } : null))
                .ForMember(e => e.ParentId, e => e.MapFrom(d => d.Control.ParentControl))
                .ForMember(e => e.ControlType, e => e.MapFrom(d => d.Control.ControlType));

            CreateMap<PageControlListDTO, PageControl>().IgnoreId();


            CreateMap<PageCategoryParameter, PageParameterEditDto>()
                .ForMember(e => e.Entity, e => e.Ignore());
            CreateMap<PageParameter, PageParameterDto>().MapChangeState();
            CreateMap<PageParameterDto, PageParameter>().IgnoreId();

            CreateMap<CustomFieldDto, CustomField>().IgnoreId();
            CreateMap<CustomField, CustomFieldDto>().MapChangeState();

            CreateMap<PageRoute, PageRouteDTO>().MapChangeState();
            CreateMap<PageRouteDTO, PageRoute>().IgnoreId();
            CreateMap<PageRouteView, PageRouteDTO>().MapChangeState();
            CreateMap<PageReferenceView, PageReferenceDTO>();

        }

        void TenantsMapping()
        {
            CreateMap<Tenant, TenantEditDTO>()
                .ForMember(e => e.Entity, e => e.MapFrom(d => d));

            CreateMap<Tenant, TenantDto>()
                .ForMember(e => e.LogoFile, e => e.MapFrom(d => new TmpFileData(d.Logo)));

            CreateMap<TenantDto, Tenant>()
                .IgnoreId()
                .ForMember(e => e.Logo, e =>
                {
                    e.PreCondition(d => d.LogoFile?.TmpPath != null);
                    e.MapFrom(d => "logos/" + d.LogoFile.Name);
                });


        }
    }
}
