using CodeShellCore.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Services
{
    public interface IDtoReadOnlyEntityService<TPrime, TOptionsDto, TListDto, TGetDto>
        where TGetDto : class
        where TListDto : class
        where TOptionsDto : PagedListRequestDto
    {
        Task<PagedResult<TListDto>> Get(TOptionsDto options);
        Task<TGetDto> GetSingle(TPrime id);
        Task<bool> IsUnique(IsUniqueDto dto);
    }
}
