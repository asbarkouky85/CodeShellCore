using System;
using System.Linq.Expressions;

using Microsoft.AspNetCore.Mvc.Rendering;

using CodeShellCore.Web.Razor.Elements.Angular;
using CodeShellCore.Web.Razor.General.Moldster;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Web.Razor.Validation;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using CodeShellCore.Moldster;

namespace CodeShellCore.Web.Razor.Elements.Moldster
{
    public class MoldsterElementsHelper : AngularElementsHelper, IElementsHelper
    {
        private IdentifierProcessor proc = new IdentifierProcessor();
        public override ControlGroupWriter AutoCompleteGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister src, bool required, int size, string alternateLabel, string placeHolder, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var d= base.AutoCompleteGroup(helper, exp, src, required, size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, "AC_TextBox");
            helper.AddSource(src);
            return d;
        }

        public override ControlGroupWriter CalendarGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes type, DateRange range, Calendars cals, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var d= base.CalendarGroup(helper, exp, type, range, cals, required, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, InputControls.CalendarTextBox);
            return d;
        }

        public override ControlGroupWriter CheckBoxGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string trueString, string falseString, bool useIcon, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var d= base.CheckBoxGroup(helper, exp, trueString, falseString, useIcon, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, InputControls.CheckBox);
            return d;
        }

        public override ControlGroupWriter DateTimeGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes type, DateRange range, Calendars cals, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var d= base.DateTimeGroup(helper, exp, type, range, cals, required, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, InputControls.DateTimeTextBox);
            return d;
        }

        public override ControlGroupWriter FileGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string formFieldName, string uploadUrl, IValidationCollection coll, int size, string alternateLabel, string placeHolder, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var d= base.FileGroup(helper, exp, formFieldName, uploadUrl, coll, size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, InputControls.FileTextBox);
            return d;
        }

        public override ControlGroupWriter RadioGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Dictionary<string, object> options, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses, string template)
        {
            var d= base.RadioGroup(helper, exp, options, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses, template);
            d.Accessibility = proc.Process(helper, exp, InputControls.CalendarTextBox);
            return d;
        }

        public override ControlGroupWriter SearchableControlGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string display, string valueMember, bool multi, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses, bool nullable)
        {
            var d= base.SearchableControlGroup(helper, exp, source, display, valueMember, multi, required, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses, nullable);
            d.Accessibility = proc.Process(helper, exp, InputControls.CalendarTextBox);
            helper.AddSource(source);
            return d;
        }

        public override ControlGroupWriter SelectControlGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string display, string valueMember, bool multi, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses, bool nullable)
        {
            var d= base.SelectControlGroup(helper, exp, source, display, valueMember, multi, required, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses, nullable);
            d.Accessibility = proc.Process(helper, exp, InputControls.CalendarTextBox);
            helper.AddSource(source);
            return d;
        }

        public override ControlGroupWriter TextAreaGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, IValidationCollection coll, int size, string alternateLabel, string placeHolder, object attrs, bool localizable, object inputAttr, string inputClasses, string groupClasses)
        {
            var d= base.TextAreaGroup(helper, exp, coll, size, alternateLabel, placeHolder, attrs, localizable, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, InputControls.CalendarTextBox);
            return d;
        }

        public override ControlGroupWriter TextBoxGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string textType, IValidationCollection coll, int size, string alternateLabel, string placeHolder, bool localizable, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var d= base.TextBoxGroup(helper, exp, textType, coll, size, alternateLabel, placeHolder, localizable, attrs, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, InputControls.CalendarTextBox);
            return d;
        }

        public override ControlGroupWriter LabelGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, int size, string filter, string alternateLabel, string url, bool blank, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var d= base.LabelGroup(helper, exp, size, filter, alternateLabel, url, blank, attrs, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, InputControls.CalendarTextBox);
            return d;
        }

        public override IHtmlContent ListView<T>(IHtmlHelper<T> helper, Expression<Func<T, IEnumerable>> exp, string listName, string display, bool mutli, string change)
        {
            var d= base.ListView(helper, exp, listName, display, mutli, change);
            var acc = proc.Process(helper, exp, "ListView");
            if (!acc.Read)
                return null;
            return d;
        }

        public override IHtmlContent ListView<T>(IHtmlHelper<T> helper, string selectionSource, string dataSource, string display, bool mutli, string onChange)
        {
            var d= base.ListView(helper, selectionSource, dataSource, display, mutli, onChange);
            var acc = proc.Process(helper, selectionSource.ToLower(), "ListView");
            if (!acc.Read)
                return null;
            return d;
        }
    }
}
