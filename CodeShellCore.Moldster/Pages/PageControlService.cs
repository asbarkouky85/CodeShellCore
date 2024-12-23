using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Services;
using CodeShellCore.Extensions.Data;
using CodeShellCore.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public class PageControlService : DtoEntityService<PageControl, long, PageControlDto, PagedListRequestDto>, IPageControlService
    {
        private readonly IMoldsterUnit _unit;
        IMoldsterLookupService lookups => Store.GetService<IMoldsterLookupService>();
        public PageControlService(IMoldsterUnit unit) : base(unit)
        {
            _unit = unit;
        }

        public async Task<PagedResult<PageControlListDTO>> GetControlByPageId(PagedListRequestDto opt)
        {
            var opts = opt.GetOptionsFor<PageControlListDTO>();
            opts.SetOrderProperty(e => e.ControlIdentifier);
            return await _unit.PageControlRepository.FindAndMap(opts);
        }

        public async Task<SubmitResult> UpdatePageControls(List<PageControlListDTO> pageControls)
        {
            var list = pageControls.MapTo<PageControl>(false);
            _unit.PageControlRepository.ApplyChanges(list);
            return await _unit.SaveChanges();
        }

        public override async Task<Dictionary<string, IEnumerable<Named<object>>>> GetEditLookups(Dictionary<string, string> dto)
        {
            return await lookups.PageControlList(dto);
        }
    }
}
