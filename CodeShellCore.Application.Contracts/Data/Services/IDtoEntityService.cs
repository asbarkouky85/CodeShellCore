using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using System.Threading.Tasks;

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
        Task<DeleteResult> Delete(TPrime id);
        Task<EntitySubmitResult<TSingleDto>> Post(TCreateDto dto);
        Task<EntitySubmitResult<TSingleDto>> Put(TUpdateDto dto);
    }


}
