
using CodeShellCore.Web.Razor.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Web.Razor.Tables.Angular
{
    
    public interface IAngularTablesHelper
    {
        CellWriter TextCell_Angular<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string pipe, object cellAttributes);
        CellWriter CheckBoxCell(IHtmlHelper helper,string field, string rowIndex, string listName, string ngModel, object cellAttributes, object inputAttr, string listItem, string classes);
        CellWriter RadioBoxCell(IHtmlHelper helper,string field, string rowIndex, string property, bool asArray, object cellAttributes, object inputAttr, string listItem, string classes);
    }
}
