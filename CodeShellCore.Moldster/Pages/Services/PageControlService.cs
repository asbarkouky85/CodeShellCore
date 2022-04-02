using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Data;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Pages.Services
{
    public class PageControlService : EntityService<PageControl>
    {
        private readonly IConfigUnit _unit;

        public PageControlService(IConfigUnit unit) : base(unit)
        {
            _unit = unit;
        }

        public LoadResult<PageControlListDTO> GetControlByPageId(LoadOptions opt)
        {
            var opts = opt.GetOptionsFor<PageControlListDTO>();
            opts.SetOrderProperty(e => e.ControlIdentifier);
            return _unit.PageControlRepository.FindAndMap(opts);
        }

        public SubmitResult UpdatePageControls(List<PageControlListDTO> pageControls)
        {
            var list = pageControls.MapTo<PageControl>(false);
            _unit.PageControlRepository.ApplyChanges(list);
            return _unit.SaveChanges();
        }
    }
}
