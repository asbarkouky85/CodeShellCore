using CodeShellCore.Data;
using CodeShellCore.Moldster.Db.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface IPageControlRepository : IRepository<PageControl>
    {
        void UpdateControls(long pageId, List<Control> controls, byte defaultAccess = 2);
        List<ControlDTO> GetDtos(Expression<Func<PageControl, bool>> filter = null);
    }
}
