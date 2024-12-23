using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Lookups
{
    public interface ILookupsAppService
    {
        Task<List<Named<object>>> Get(string entity, string collectionId = null);
        Task<PagedResult<Named<object>>> GetPaged(string entity, LookupsPagedResultRequestDto dto);
    }
}
