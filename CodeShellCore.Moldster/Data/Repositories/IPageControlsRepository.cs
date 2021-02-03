using CodeShellCore.Data;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Moldster.Data.Repositories
{
    public interface IPageControlRepository : IRepository<PageControl>
    {
        void UpdateControls(long pageId, List<Control> controls, byte defaultAccess = 2);
        List<ControlDTO> GetDtos(Expression<Func<PageControl, bool>> filter = null);
    }
}
