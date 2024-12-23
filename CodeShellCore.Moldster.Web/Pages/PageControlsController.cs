using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Web.Controllers;

namespace CodeShellCore.Moldster.Pages
{
    public class PageControlsController : BaseApiController, IPageControlService
    {
        IPageControlService _service;

        public PageControlsController(IPageControlService service)
        {
            _service = service;
        }

        public Task<DeleteResult> Delete(long id)
        {
            return _service.Delete(id);
        }

        public Task<PagedResult<PageControlDto>> Get(PagedListRequestDto options)
        {
            return _service.Get(options);
        }

        public Task<PagedResult<PageControlDto>> GetCollection(string id, PagedListRequestDto options)
        {
            return _service.GetCollection(id, options);
        }

        public Task<PagedResult<PageControlListDTO>> GetControlByPageId(PagedListRequestDto opt)
        {
            return _service.GetControlByPageId(opt);
        }

        public Task<Dictionary<string, IEnumerable<Named<object>>>> GetEditLookups(Dictionary<string, string> data)
        {
            return _service.GetEditLookups(data);
        }

        public Task<Dictionary<string, IEnumerable<Named<object>>>> GetListLookups(Dictionary<string, string> data)
        {
            return _service.GetListLookups(data);
        }

        public Task<PageControlDto> GetSingle(long id)
        {
            return _service.GetSingle(id);
        }

        public Task<bool> IsUnique(IsUniqueDto dto)
        {
            return _service.IsUnique(dto);
        }

        public Task<EntitySubmitResult<PageControlDto>> Post(PageControlDto dto)
        {
            return _service.Post(dto);
        }

        public Task<EntitySubmitResult<PageControlDto>> Put(PageControlDto dto)
        {
            return _service.Put(dto);
        }

        public Task<SubmitResult> UpdatePageControls(List<PageControlListDTO> pageControls)
        {
            return _service.UpdatePageControls(pageControls);
        }
    }
}
