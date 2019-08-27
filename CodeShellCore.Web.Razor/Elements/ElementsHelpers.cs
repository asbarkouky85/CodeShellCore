using System;
using System.Collections;
using System.Linq.Expressions;
using System.Collections.Generic;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Web.Razor.Validation;
using CodeShellCore.Moldster;

namespace CodeShellCore.Web.Razor.Elements
{
    public static class ElementsHelpers
    {
        static IElementsHelper Provider { get { return Shell.ScopedInjector.GetRequiredService<IElementsHelper>(); } }

        public static IHtmlContent TextAreaGroup<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            IValidationCollection coll = null,
            int size = -1,
            string alternateLabel = null,
            string placeHolder = null,
            object attrs = null,
            bool localizable = false,
            object inputAttr = null,
            string inputClasses = "",
            string groupClasses = "") where T : class
        {
            var elem = Provider.TextAreaGroup(helper, exp, coll, size, alternateLabel, placeHolder, attrs, localizable, inputAttr, inputClasses, groupClasses);
            return elem.Write(InputControls.Textarea, localizable);
        }



        public static IHtmlContent LabelGroup<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            int size = -1,
            string filter = null,
            string alternateLabel = null,
            string url = null,
            bool blank = false,
            object attrs = null,
            object inputAttr = null,
            string inputClasses = "",
            string groupClasses = "")
        {
            var elem = Provider.LabelGroup(helper, exp, size, filter, alternateLabel, url, blank, attrs, inputAttr, inputClasses, groupClasses);
            return elem.Write(InputControls.Label);
        }

        public static IHtmlContent ControlGroup<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            string textType = "text",
            IValidationCollection coll = null,
            int size = -1,
            string alternateLabel = null,
            string placeHolder = null,
            bool localizable = false,
            object attrs = null,
            object inputAttr = null,
            string inputClasses = "",
            string groupClasses = "")
        {
            var elem = Provider.TextBoxGroup(helper, exp, textType, coll, size, alternateLabel, placeHolder, localizable, attrs, inputAttr, inputClasses, groupClasses);
            return elem.Write(InputControls.TextBox, localizable);
        }



        public static IHtmlContent AutoComplete<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            Lister src,
            bool required = false,
            int size = -1,
            string alternateLabel = null,
            string placeHolder = null,
            object attrs = null,
            object inputAttr = null,
            string inputClasses = "",
            string groupClasses = "")
        {
            var elem = Provider.AutoCompleteGroup(helper, exp, src, required, size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);
            return elem.Write(InputControls.TextBox);
        }

        public static IHtmlContent CheckBoxGroup<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            string trueString = "Yes",
            string falseString = "No",
            bool useIcon = true,
            int size = -1,
            string alternateLabel = null,
            object attrs = null,

            object inputAttr = null,
            string inputClasses = "",
            string groupClasses = "")
        {
            var elem = Provider.CheckBoxGroup(helper, exp, trueString, falseString, useIcon, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses);
            return elem.Write(InputControls.CheckBox);
        }

        public static IHtmlContent InputControl<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            InputControls component,
            string textType = "text",
            Dictionary<string, object> radioOptions = null,
            int size = -1,
            string alternateLabel = null,
            string placeHolder = null,
            object attrs = null,
            object inputAttr = null,
            string inputClasses = "",
            string groupClasses = "")
        {
            return Provider.InputControl(helper, exp, component, textType, radioOptions, size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);
        }

        public static IHtmlContent FileGroup<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            string formFieldName,
            string uploadUrl = "Files/Upload",
            IValidationCollection coll = null,
            int size = -1,
            string alternateLabel = null,
            string placeHolder = null,
            object attrs = null,
            object inputAttr = null,
            string inputClasses = "",
            string groupClasses = "")
        {
            var elem = Provider.FileGroup(helper, exp, formFieldName, uploadUrl, coll, size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);
            return elem.Write(InputControls.FileTextBox);
        }

        public static IHtmlContent RadioGroup<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            Dictionary<string, object> options,
            int size = -1,
            string alternateLabel = null,
            object attrs = null,
            object inputAttr = null,
            string inputClasses = "",
            string groupClasses = "",
            string template = "")
        {
            var elem = Provider.RadioGroup(helper, exp, options, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses, template);
            return elem.Write(InputControls.Radio);
        }

        public static IHtmlContent CalendarGroup<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            CalendarTypes type = CalendarTypes.PastAndFuture,
            DateRange range = null,
            Calendars cals = Calendars.Greg,
            bool required = false,
            int size = -1,
            string alternateLabel = null,
            string placeHolder = null,
            object attrs = null,
            object inputAttr = null,
            string inputClasses = "",
            string groupClasses = "")
        {
            var elem = Provider.CalendarGroup(helper, exp, type, range, cals, required, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses);
            return elem.Write(InputControls.CalendarTextBox);
        }

        public static IHtmlContent DateTimeGroup<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            CalendarTypes type = CalendarTypes.PastAndFuture,
            DateRange range = null,
            Calendars cals = Calendars.Greg,
            bool required = false,
            int size = -1,
            string alternateLabel = null,
            string placeHolder = null,
            object attrs = null,
            object inputAttr = null,
            string inputClasses = "",
            string groupClasses = "")
        {
            var elem = Provider.DateTimeGroup(helper, exp, type, range, cals, required, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses);
            return elem.Write(InputControls.DateTimeTextBox);
        }



        public static IHtmlContent SelectControlGroup<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            Lister source,
            string display,
            string valueMember = null,
            bool multi = false,
            bool required = false,
            int size = -1,
            string alternateLabel = null,
            object attrs = null,
            object inputAttr = null,
            string inputClasses = "",
            string groupClasses = "",
            string idExtra = "",
            string readOnlyProperty = null,
            bool nullable = true)
        {
            var elem = Provider.SelectControlGroup(helper, exp, source, display, valueMember, multi, required, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses, nullable);
            return elem.Write(InputControls.Select);
        }

        public static IHtmlContent SearchableControlGroup<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            Lister source,
            string display,
            string valueMember = null,
            bool multi = false,
            bool required = false,
            int size = -1,
            string alternateLabel = null,
            object attrs = null,
            object inputAttr = null,
            string inputClasses = "",
            string groupClasses = "",
            string idExtra = "",
            string readOnlyProperty = null,
            bool nullable = false)
        {
            var elem = Provider.SearchableControlGroup(helper, exp, source, display, valueMember, multi, required, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses, nullable);
            return elem.Write(InputControls.Select_Searchable);
        }

        public static IHtmlContent ListView<T>(this IHtmlHelper<T> helper, string selectionSource,
            string dataSource,
            string display,
            bool mutli = false,
            string onChange = "Default")
        {
            return Provider.ListView(helper, selectionSource, dataSource, display, mutli, onChange);
        }

        public static IHtmlContent ListView<T>(this IHtmlHelper<T> helper, Expression<Func<T, IEnumerable>> exp,
            string listName,
            string display,
            bool mutli = false,
            string change = "Default",
            string displayPipe = null)
        {
            return Provider.ListView(helper, exp, listName, display, mutli, change);

        }
    }
}
