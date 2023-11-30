using AutoMapper;
using CodeShellCore.Data.Events;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore
{
    public class CodeShellAutoMapperProfile : Profile
    {
        public CodeShellAutoMapperProfile()
        {
            CreateMap(typeof(CrudEvent<>), typeof(CrudEvent<>));
            CreateMap(typeof(LoadResult<>), typeof(LoadResult<>));
        }
    }
}
