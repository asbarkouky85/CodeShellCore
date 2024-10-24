using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public interface ILookupsAppService
    {
        List<Named<object>> Get(string entity, string collectionId = null);
        PagedResult<Named<object>> GetPaged(string entity, LookupsPagedResultRequestDto dto);
    }
}
