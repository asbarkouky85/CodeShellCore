using AutoMapper;
using CodeShellCore.Data.Events;
using CodeShellCore.Data.Localization;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Linq.Filtering;
using System;
using System.Collections.Generic;

namespace CodeShellCore
{
    public class CodeShellAutoMapperProfile : Profile
    {
        public CodeShellAutoMapperProfile()
        {
            CreateMap(typeof(CrudEvent<>), typeof(CrudEvent<>));
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
            CreateMap(typeof(Named<>), typeof(Named<>));

            CreateMap<PagedListRequestDto, PagedListRequest>()
                .ForMember(e => e.Direction, e => e.MapFrom(d => Enum.Parse<SortDir>(d.Direction)));
            CreateMap<PropertyFilterDto, PropertyFilter>();

            CreateMap<LocalizablesData, LocalizablesDto>();
            CreateMap<LocalizablesDto, LocalizablesData>();



        }
    }
}
