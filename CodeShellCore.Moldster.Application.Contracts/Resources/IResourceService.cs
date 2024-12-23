using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Resources
{
    public interface IResourceService : IDtoEntityService<long, PagedListRequestDto, ResourceListDTO, ResourceDto, ResourceDto, ResourceDto>
    {

    }
}