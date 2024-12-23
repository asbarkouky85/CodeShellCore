using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Lookups
{
    public class LookupsAppService : ApplicationService, ILookupsAppService
    {
        ILookupsService Lookups => Store.GetRequiredService<ILookupsService>();
        public LookupsAppService(IServiceProvider provider) : base(provider)
        {
        }

        public async Task<List<Named<object>>> Get(string entity, string collectionId = null)
        {
            return (await Lookups.GetListNamed(entity, collectionId)).ToList();
        }

        public async Task<PagedResult<Named<object>>> GetPaged(string entity, LookupsPagedResultRequestDto dto)
        {
            return await Lookups.GetListNamedPaged(entity, dto, dto.CollectionId);
        }
    }
}
