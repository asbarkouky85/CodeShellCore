using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodeShellCore.Moldster.Resources
{
    public class ResourceService : DtoEntityService<Resource, long, PagedListRequestDto, ResourceListDTO, ResourceDto, ResourceDto, ResourceDto>, IResourceService
    {
        IMoldsterUnit _unit;
        public ResourceService(IMoldsterUnit unit) : base(unit)
        {
            _unit = unit;
        }

        public override async Task<PagedResult<ResourceListDTO>> Get(PagedListRequestDto options)
        {
            return await _unit.ResourceRepository.FindAndMap(options.GetOptionsFor<ResourceListDTO>());
        }

        public override async Task<Dictionary<string, IEnumerable<Named<object>>>> GetEditLookups(Dictionary<string, string> dto)
        {
            return await Store.GetService<IMoldsterLookupService>().ResourceEdit(dto);
        }
    }
}
