using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public class LookupsAppService : ApplicationService, ILookupsAppService
    {
        ILookupsService Lookups => Store.GetRequiredService<ILookupsService>();
        public LookupsAppService(IServiceProvider provider) : base(provider)
        {
        }

        public List<NamedDto<object>> Get(string entity, string collectionId = null)
        {
            return Lookups.GetListNamed(entity, collectionId).ToList();
        }

        public PagedResult<NamedDto<object>> GetPaged(string entity, LookupsPagedResultRequestDto dto)
        {
            return Lookups.GetListNamedPaged(entity, dto, dto.CollectionId);
        }
    }
}
