
using CodeShellCore.Moldster;
using CodeShellCore.Web.Razor.Validation;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Web.Razor.Tables
{
    public interface ITablesHelper
    {
        CellWriter CalendarCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes rangeType, Calendars cals, DateRange range, bool required, object cellAttributes, object inputAttr, string classes);
        CellWriter DragContentCell<T>(IHtmlHelper<T> helper, string tableName, string modelName, string width, object cellAttributes);
        CellWriter DragHeaderCell<T>(IHtmlHelper<T> helper, string tableName, string width, object cellAttributes);
        CellWriter HeaderCell(IHtmlHelper helper, string text, string size, bool isColumn, object cellAttributes,bool sorting,bool isRequired);
        CellWriter HeaderCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string size, object cellAttributes,bool sorting,bool isRequired);
        CellWriter LinkCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string url, bool blank, object cellAttributes, object linkAttributes, string pipe);
        CellWriter SelectCell<T, TValue>( IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string displayMember, string valueMember, bool required, bool multi, object cellAttributes, object inputAttr, string classes,bool nullable, string rowIndex);
        CellWriter TextBoxCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string textBoxType,string rowIndex, IValidationCollection coll, object cellAttributes, object inputAttr, string classes);
        CellWriter TextCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, object cellAttributes);
        IHtmlContent HeaderRow(IHtmlHelper helper, params string[] cols);
    }
}
