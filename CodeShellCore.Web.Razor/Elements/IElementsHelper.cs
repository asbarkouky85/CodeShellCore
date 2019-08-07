using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CodeShellCore.Web.Razor.Validation;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.Elements
{
    public interface IElementsHelper
    {
        ControlGroupWriter TextAreaGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, IValidationCollection coll, int size, string alternateLabel, string placeHolder, object attrs, bool localizable, object inputAttr, string inputClasses, string groupClasses) where T : class;
        ControlGroupWriter LabelGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, int size, string filter, string alternateLabel,string url,bool blank, object attrs, object inputAttr, string inputClasses, string groupClasses);
        ControlGroupWriter TextBoxGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string textType, IValidationCollection coll, int size, string alternateLabel, string placeHolder, bool localizable, object attrs, object inputAttr, string inputClasses, string groupClasses);
        ControlGroupWriter AutoCompleteGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister src, bool required, int size, string alternateLabel, string placeHolder, object attrs, object inputAttr, string inputClasses, string groupClasses);
        ControlGroupWriter CheckBoxGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string trueString, string falseString, bool useIcon, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses);
        ControlGroupWriter FileGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string formFieldName, string uploadUrl, IValidationCollection coll, int size, string alternateLabel, string placeHolder, object attrs, object inputAttr, string inputClasses, string groupClasses);
        ControlGroupWriter SearchableControlGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string display, string valueMember, bool multi, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses, bool nullable);
        ControlGroupWriter SelectControlGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string display, string valueMember, bool multi, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses, bool nullable);
        ControlGroupWriter DateTimeGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes type, DateRange range, Calendars cals, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses);
        ControlGroupWriter CalendarGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes type, DateRange range, Calendars cals, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses);
        ControlGroupWriter RadioGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Dictionary<string, object> options, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses, string template);
        IHtmlContent ListView<T>(IHtmlHelper<T> helper, Expression<Func<T, IEnumerable>> exp, string listName, string display, bool mutli, string change);
        IHtmlContent ListView<T>(IHtmlHelper<T> helper, string selectionSource, string dataSource, string display, bool mutli, string onChange);
        IHtmlContent InputControl<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, InputControls component, string textType, Dictionary<string, object> radioOptions, int size, string alternateLabel, string placeHolder, object attrs, object inputAttr, string inputClasses, string groupClasses);
    }
}
