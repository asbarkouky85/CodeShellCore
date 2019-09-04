
using CodeShellCore.Web.Razor.Models;
using CodeShellCore.Web.Razor.Tables;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Web.Razor.Tables.Angular
{

    public interface IAngularTablesHelper : ITablesHelper
    {
        CellWriter TextCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string pipe, object cellAttributes);
        CellWriter CheckBoxCell<T>(IHtmlHelper<T> helper, string field, string rowIndex, string listName, string ngModel, object cellAttributes, object inputAttr, string listItem, string classes);
        CellWriter RadioBoxCell<T>(IHtmlHelper<T> helper, string field, string rowIndex, string property, bool asArray, object cellAttributes, object inputAttr, string listItem, string classes);
        IHtmlContent ListModifiers<T>(IHtmlHelper<T> helper, string idExpression, IEnumerable<LinkModel> buttons, string detailsFunction, string editFunction, string deleteFunction, string identifier, string modifiers, string permissionName, string classes);
    }
}
