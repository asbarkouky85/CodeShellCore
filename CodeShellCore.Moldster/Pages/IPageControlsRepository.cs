using CodeShellCore.Data;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Moldster.Data.Repositories
{
    public interface IPageControlRepository : IRepository<PageControl>
    {
        void UpdateControls(long pageId, List<Control> controls, byte defaultAccess = 2);
        List<ControlRenderDto> GetDtos(Expression<Func<PageControl, bool>> filter = null);
    }
}
