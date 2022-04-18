using AutoMapper;
using CodeShellCore.Files;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Pages.Dtos;
using CodeShellCore.Moldster.Tenants;

namespace CodeShellCore.Moldster
{
    public class MoldsterMappingProfile : Profile
    {
        public MoldsterMappingProfile()
        {
            DomainsMapping();
            LocalizationMapping();
            PagesMapping();
            TenantsMapping();
        }

        void DomainsMapping()
        {
            CreateMap<Domain, DomainWithPagesDTO>()
                .ForMember(e => e.DomainName, e => e.MapFrom(d => d.Name))
                .ForMember(e => e.Pages, d => d.MapFrom(e => e.Pages));

        }

        void LocalizationMapping()
        {
            CreateMap<CustomField, CustomFieldDto>();
        }

        void PagesMapping()
        {
            CreateMap<Page, PageDto>();
            CreateMap<Page, PageDetailsDto>()
                .ForMember(e => e.ParentHasResource, e => e.MapFrom(p => p.PageCategory.ResourceId != null))
                .ForMember(e => e.BaseViewPath, e => e.MapFrom(p => p.PageCategory.ViewPath))
                .ForMember(e => e.ActionName, e => e.MapFrom(p => p.ResourceAction == null ? (p.SpecialPermission ?? null) : p.ResourceAction.Name))
                .ForMember(e => e.PageIdentifier, e => e.MapFrom(p => p.Domain.Name + "__" + p.Name))
                .ForMember(e => e.ComponentName, d => d.MapFrom(e => e.SourceCollection == null ? null : e.SourceCollection.Name))
                .ForMember(e => e.Page, d => d.MapFrom(e => e));

            CreateMap<PageControl, PageControlListDTO>()
                .ForMember(e => e.ControlType, d => d.MapFrom(e => e.Control.ControlType));

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
