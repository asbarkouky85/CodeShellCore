using AutoMapper;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Domains.Dtos;
using CodeShellCore.Moldster.Localization.Dtos;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Pages.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster
{
    public class MoldsterMappingProfile : Profile
    {
        public MoldsterMappingProfile()
        {
            CreateMap<Page, PageDto>();
            CreateMap<Page, PageDetailsDto>()
                .ForMember(e => e.ParentHasResource, e => e.MapFrom(p => p.PageCategory.ResourceId != null))
                .ForMember(e => e.BaseViewPath, e => e.MapFrom(p => p.PageCategory.ViewPath))
                .ForMember(e => e.ActionName, e => e.MapFrom(p => p.ResourceAction == null ? (p.SpecialPermission ?? null) : p.ResourceAction.Name))
                .ForMember(e => e.PageIdentifier, e => e.MapFrom(p => p.Domain.Name + "__" + p.Name))
                .ForMember(e => e.ComponentName, d => d.MapFrom(e => e.SourceCollection == null ? null : e.SourceCollection.Name))
                .ForMember(e => e.Page, d => d.MapFrom(e => e));

            CreateMap<Domain, DomainWithPagesDTO>()
                .ForMember(e => e.DomainName, e => e.MapFrom(d => d.Name))
                .ForMember(e => e.Pages, d => d.MapFrom(e => e.Pages));

            CreateMap<PageControl, PageControlListDTO>()
                .ForMember(e => e.ControlType, d => d.MapFrom(e => e.Control.ControlType));

            CreateMap<CustomField, CustomFieldDto>();
        }
    }
}
