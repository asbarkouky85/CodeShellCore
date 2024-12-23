using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Web.Controllers;

namespace CodeShellCore.Moldster.Resources
{
    public class ResourcesController : BaseApiController, IResourceService
    {
        readonly IResourceService _service;

        public ResourcesController(IResourceService service)
        {
            _service = service;
        }

        public Task<DeleteResult> Delete(long id)
        {
            return _service.Delete(id);
        }

        public Task<PagedResult<ResourceListDTO>> Get(PagedListRequestDto options)
        {
            return _service.Get(options);
        }

        public Task<PagedResult<ResourceListDTO>> GetCollection(string id, PagedListRequestDto options)
        {
            return _service.GetCollection(id, options);
        }

        public Task<Dictionary<string, IEnumerable<Named<object>>>> GetEditLookups(Dictionary<string, string> data)
        {
            return _service.GetEditLookups(data);
        }

        public Task<Dictionary<string, IEnumerable<Named<object>>>> GetListLookups(Dictionary<string, string> data)
        {
            return _service.GetListLookups(data);
        }

        public Task<ResourceDto> GetSingle(long id)
        {
            return _service.GetSingle(id);
        }

        public Task<bool> IsUnique(IsUniqueDto dto)
        {
            return _service.IsUnique(dto);
        }

        public Task<EntitySubmitResult<ResourceDto>> Post(ResourceDto dto)
        {
            return _service.Post(dto);
        }

        public Task<EntitySubmitResult<ResourceDto>> Put(ResourceDto dto)
        {
            return _service.Put(dto);
        }
    }
}
