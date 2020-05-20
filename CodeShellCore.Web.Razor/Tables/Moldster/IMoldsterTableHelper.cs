using CodeShellCore.Moldster;
using CodeShellCore.Web.Razor.Containers;
using CodeShellCore.Web.Razor.Tables.Angular;
using CodeShellCore.Web.Razor.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Web.Razor.Tables.Moldster
{
    public interface IMoldsterTableHelper : IAngularTablesHelper
    {
        CellWriter HeaderCell<T>(IHtmlHelper<T> helper, MoldsterHtmlContainer parent, string textId, bool isColumn = false, string size = null, object cellAttributes = null, bool ignoreAccessibility = false, bool sorting = true);
        CellWriter SelectCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string displayMember, string valueMember, MoldsterHtmlContainer parent, bool required, bool multi, bool nullable, object cellAttributes, object inputAttr, string classes, string readOnlyProp, string rowIndex);
        CellWriter AutoCompleteSelectCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string displayMember, string valueMember, MoldsterHtmlContainer parent, bool required, bool multi, bool nullable, object cellAttributes, object inputAttr, string classes);
        CellWriter TextBoxCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, MoldsterHtmlContainer parent, string textBoxType, IValidationCollection coll, object cellAttributes, object inputAttr, string classes, string rowIndex, bool localizable);
        CellWriter HeaderCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, MoldsterHtmlContainer parent, string width, bool ignoreAccessibility, object cellAttributes, bool sorting = true);
        CellWriter TextCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, MoldsterHtmlContainer parent, string pipe, object cellAttributes);
        CellWriter CalendarCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, MoldsterHtmlContainer parent, CalendarTypes rangeType, Calendars cals, DateRange range, bool required, object cellAttributes, object inputAttr, string classes);
    }
}
