using CodeShellCore.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Services
{
    public interface IDtoCollectionsEntityService<TPrime, TOptionsDto, TListDto>
        where TListDto : class
        where TOptionsDto : PagedListRequestDto
    {
        Task<PagedResult<TListDto>> GetCollection(string id, TOptionsDto options);
    }
}
