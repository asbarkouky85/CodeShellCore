using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Services
{
    public interface IDtoEntityService<T, TPrime, TDto> :
       IDtoEntityService<T, TPrime, LoadOptions, TDto, TDto, TDto, TDto>

       where T : class, IModel<TPrime>
       where TDto : class
    {

    }

    public interface IDtoEntityService<T, TPrime, TDto, TOptionsDto> :
        IDtoEntityService<T, TPrime, TOptionsDto, TDto, TDto, TDto, TDto>

        where T : class, IModel<TPrime>
        where TDto : class
       where TOptionsDto : LoadOptions
    {

    }

    public interface IDtoEntityService<T, TPrime, TDto, TOptionsDto, TSingleDto> :
        IDtoEntityService<T, TPrime, TOptionsDto, TDto, TSingleDto, TSingleDto, TSingleDto>

        where T : class, IModel<TPrime>
        where TDto : class
        where TSingleDto : class
        where TOptionsDto : LoadOptions
    {

    }

    public interface IDtoEntityService<T, TPrime, TDto, TOptionsDto, TSingleDto, TCreateUpdateDto> :
        IDtoEntityService<T, TPrime, TOptionsDto, TDto, TSingleDto, TCreateUpdateDto, TCreateUpdateDto>

        where T : class, IModel<TPrime>
        where TDto : class
        where TSingleDto : class
        where TCreateUpdateDto : class
        where TOptionsDto : LoadOptions
    {

    }
}
