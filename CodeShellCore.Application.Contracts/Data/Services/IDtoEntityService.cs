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
        where TOptionsDto : PagedListRequestDto
    {
        DeleteResult Delete(TPrime id);
        EntitySubmitResult<TSingleDto> Post(TCreateDto dto);
        EntitySubmitResult<TSingleDto> Put(TUpdateDto dto);
    }


}
