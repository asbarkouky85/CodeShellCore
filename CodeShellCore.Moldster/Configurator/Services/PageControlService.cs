using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Db.Dto;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Configurator.Services
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
            return _unit.PageControlRepository.FindAs(PageControlListDTO.Expression, opts);
        }

        public SubmitResult UpdatePageControls(List<PageControlListDTO> pageControls)
        {
            var list = pageControls.MapTo<PageControl>(false);
            _unit.PageControlRepository.ApplyChanges(list);
            return _unit.SaveChanges();
        }
    }
}
