using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public interface ILookupsAppService
    {
        List<NamedDto<object>> Get(string entity, string collectionId = null);
        PagedResult<NamedDto<object>> GetPaged(string entity, LookupsPagedResultRequestDto dto);
    }
}
