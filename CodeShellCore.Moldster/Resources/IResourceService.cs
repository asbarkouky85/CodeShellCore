using CodeShellCore.Data.Services;
using CodeShellCore.Linq;

namespace CodeShellCore.Moldster.Resources
{
    public interface IResourceService : IDtoEntityService<long, PagedListRequestDto, ResourceListDTO, ResourceDto, ResourceDto, ResourceDto>
    {

    }
}