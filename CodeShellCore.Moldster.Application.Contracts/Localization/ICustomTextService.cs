using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Localization
{
    public interface ICustomTextService
    {
        Task<PagedResult<CustomTextDto>> Get(CustomTextRequestDto req, PagedListRequestDto opts);
        Task<SubmitResult> SaveChanges(IEnumerable<CustomTextDto> lst);
    }
}
