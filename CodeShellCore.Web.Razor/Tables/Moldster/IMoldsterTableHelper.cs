using CodeShellCore.Moldster;
using CodeShellCore.Web.Razor.Containers;
using CodeShellCore.Web.Razor.Tables.Angular;
using CodeShellCore.Web.Razor.Validation;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Web.Razor.Tables.Moldster
{
    public interface IMoldsterTableHelper : IAngularTablesHelper
    {
        CellWriter AutoCompleteSelectCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string displayMember, string valueMember, bool required, bool multi, bool nullable, object cellAttributes, object inputAttr, string classes);
        CellWriter CalendarCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes rangeType, Calendars cals, DateRange range, bool required, object cellAttributes, object inputAttr, string classes, string rowIndex);
        CellWriter HeaderCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp , string width, bool ignoreAccessibility, object cellAttributes, bool sorting = true, bool isRequired = false);
        CellWriter HeaderCell<T>(IHtmlHelper<T> helper, string textId, bool isColumn = false, string size = null, object cellAttributes = null, bool ignoreAccessibility = false, bool sorting = true, bool isRequired = false);
        CellWriter SelectCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string displayMember, string valueMember, bool required, bool multi, bool nullable, object cellAttributes, object inputAttr, string classes, string readOnlyProp, string rowIndex);
        CellWriter TextBoxCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string textBoxType, IValidationCollection coll, object cellAttributes, object inputAttr, string classes, string rowIndex, bool localizable);
       
    }
}
