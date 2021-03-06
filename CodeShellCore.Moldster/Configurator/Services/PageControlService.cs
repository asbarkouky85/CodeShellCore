﻿using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
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
            return (LoadResult<PageControlListDTO>)_unit.PageControlRepository.FindAsSorted(PageControlListDTO.Expression, d => d.Control.Identifier, SortDir.ASC, opts);
        }

        public SubmitResult UpdatePageControls(List<PageControlListDTO> pageControls)
        {
            var list = pageControls.MapTo<PageControl>(false);
            _unit.PageControlRepository.ApplyChanges(list);
            return _unit.SaveChanges();
        }
    }
}
