
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
        IHtmlContent HeaderRow(IHtmlHelper helper, params string[] cols);
        CellWriter HeaderCell(IHtmlHelper helper, string text, string size, bool isColumn, object cellAttributes);
        CellWriter HeaderCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string size, object cellAttributes);
        CellWriter TextCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, object cellAttributes);
        CellWriter SelectCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string displayMember, string valueMember, bool required, bool multi, object cellAttributes, object inputAttr, string classes);
        CellWriter TextBoxCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string textBoxType, IValidationCollection coll, object cellAttributes, object inputAttr, string classes);
        CellWriter CalendarCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes rangeType, Calendars cals, DateRange range, bool required, object cellAttributes, object inputAttr, string classes);
    }
}
