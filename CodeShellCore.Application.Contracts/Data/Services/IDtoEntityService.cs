using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using System;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.Services
{

    public interface IDtoEntityService<T, TPrime, TOptionsDto, TListDto, TSingleDto, TCreateDto, TUpdateDto> :
        IDtoReadOnlyEntityService<T, TPrime, TOptionsDto, TListDto, TSingleDto>,
        IHasLookupsEntityService,
        IDtoCollectionsEntityService<T, TPrime, TOptionsDto, TListDto>

        where T : class, IModel<TPrime>
        where TSingleDto : class
        where TListDto : class
        where TCreateDto : class
        where TUpdateDto : class
        where TOptionsDto : LoadOptions
    {
        DeleteResult Delete(TPrime prime);
        SubmitResult<TSingleDto> Post(TCreateDto obj);
        SubmitResult<TSingleDto> Put(TUpdateDto obj);
    }

   
}
