using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Services
{
    public interface IDtoEntityService< TPrime, TDto> :
       IDtoEntityService< TPrime, LoadOptions, TDto, TDto, TDto, TDto>
       where TDto : class
    {

    }

    public interface IDtoEntityService<TPrime, TDto, TOptionsDto> :
        IDtoEntityService<TPrime, TOptionsDto, TDto, TDto, TDto, TDto>
        where TDto : class
       where TOptionsDto : LoadOptions
    {

    }

    public interface IDtoEntityService<TPrime, TDto, TOptionsDto, TSingleDto> :
        IDtoEntityService<TPrime, TOptionsDto, TDto, TSingleDto, TSingleDto, TSingleDto>
        where TDto : class
        where TSingleDto : class
        where TOptionsDto : LoadOptions
    {

    }

    public interface IDtoEntityService<TPrime, TDto, TOptionsDto, TSingleDto, TCreateUpdateDto> :
        IDtoEntityService<TPrime, TOptionsDto, TDto, TSingleDto, TCreateUpdateDto, TCreateUpdateDto>
        where TDto : class
        where TSingleDto : class
        where TCreateUpdateDto : class
        where TOptionsDto : LoadOptions
    {

    }
}
