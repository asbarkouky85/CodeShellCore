using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;

namespace CodeShellCore.Data.Services
{

    public interface IDtoEntityService<TPrime, TOptionsDto, TListDto, TSingleDto, TCreateDto, TUpdateDto> :
        IDtoReadOnlyEntityService<TPrime, TOptionsDto, TListDto, TSingleDto>,
        IHasLookupsEntityService,
        IDtoCollectionsEntityService<TPrime, TOptionsDto, TListDto>

        where TSingleDto : class
        where TListDto : class
        where TCreateDto : class
        where TUpdateDto : class
        where TOptionsDto : LoadOptions
    {
        DeleteResult Delete(TPrime id);
        SubmitResult<TSingleDto> Post(TCreateDto dto);
        SubmitResult<TSingleDto> Put(TUpdateDto dto);
    }


}
