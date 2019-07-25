
using CodeShellCore.Web.Razor.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Web.Razor.Tables.Angular
{
    public interface IAngularTablesHelper
    {
        CellWriter TextCell_Angular<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string pipe, object cellAttributes);
    }
}
