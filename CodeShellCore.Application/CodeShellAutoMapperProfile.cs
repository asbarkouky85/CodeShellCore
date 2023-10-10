using AutoMapper;
using CodeShellCore.MQ.Events;
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
        }
    }
}
