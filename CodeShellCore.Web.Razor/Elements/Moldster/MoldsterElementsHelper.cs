using CodeShellCore.Moldster;
using CodeShellCore.Web.Razor.Elements.Angular;
using CodeShellCore.Web.Razor.General.Moldster;
using CodeShellCore.Web.Razor.Models;
using CodeShellCore.Web.Razor.Validation;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Web.Razor.Elements.Moldster
{
    public class MoldsterElementsHelper : AngularElementsHelper, IElementsHelper, IMoldsterElementHelper
    {
        private IdentifierProcessor proc = new IdentifierProcessor();
        public override ControlGroupWriter AutoCompleteGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister src, IValidationCollection coll, int size, string alternateLabel, string placeHolder, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var d = base.AutoCompleteGroup(helper, exp, src, coll, size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, "AC_TextBox");
            helper.AddSource(src);
            return d;
        }

        public override ControlGroupWriter CustomInputGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string componentName, bool required, int size, object attrs, object inputAttrs, string classes)
        {
            var wt = base.CustomInputGroup(helper, exp, componentName, required, size, attrs, inputAttrs, classes);
            wt.Accessibility = proc.Process(helper, exp, componentName);
            return wt;
        }

        public override ControlGroupWriter RichTextBox<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> expression, string modules = null, int size = 6, object attrs = null, object inputAttrs = null, string classes = null)
        {
            var wt = base.RichTextBox(helper, expression, modules, size, attrs, inputAttrs, classes);
            wt.Accessibility = proc.Process(helper, expression, "RichTextBox");
            return wt;
        }

        public override ControlGroupWriter RichLabel<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, int size, object attrs, object inputAttrs, string classes)
        {
            var wt = base.RichLabel(helper, exp, size, attrs, inputAttrs, classes);
            wt.Accessibility = proc.Process(helper, exp, "RichLabel");
            return wt;
        }

        public override IHtmlContent SelectModalButton<T>(IHtmlHelper<T> helper, string textId, string function, string bind, object attrs, string identifier, string validationFunction, string url, IEnumerable<LinkModel> buttons = null)
        {
            if (identifier != null)
            {
                var acc = proc.Process(helper, identifier, "SelectModal");
                if (!acc.Read)
                    return null;
                if (!acc.Write)
                    function = null;
            }
            return base.SelectModalButton(helper, textId, function, bind, attrs, identifier, validationFunction, url, buttons);
        }

        public override ControlGroupWriter CalendarGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes type, DateRange range, Calendars cals, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var d = base.CalendarGroup(helper, exp, type, range, cals, required, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, InputControls.CalendarTextBox);
            return d;
        }

        public override ControlGroupWriter CheckBoxGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string trueString, string falseString, bool useIcon, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var d = base.CheckBoxGroup(helper, exp, trueString, falseString, useIcon, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, InputControls.CheckBox);
            return d;
        }

        public override ControlGroupWriter DateTimeGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes type, DateRange range, Calendars cals, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var d = base.DateTimeGroup(helper, exp, type, range, cals, required, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, InputControls.DateTimeTextBox);
            return d;
        }

        public override ControlGroupWriter FileGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string formFieldName, string uploadAction, bool required, bool multiple, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var d = base.FileGroup(helper, exp, formFieldName, uploadAction, required, multiple, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, InputControls.FileTextBox);
            return d;
        }

        public override ControlGroupWriter RadioGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Dictionary<string, object> options, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses, string template, string readOnlyProp, string readOnlyPipe)
        {
            var d = base.RadioGroup(helper, exp, options, required, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses, template, readOnlyProp, readOnlyPipe);
            d.Accessibility = proc.Process(helper, exp, InputControls.Radio);
            if (!d.Accessibility.Write)
            {
                if (readOnlyProp != null)
                {
                    d.InputModel.NgModelName = (readOnlyPipe == "translate" ? "'Words.'+" : "") + d.InputModel.NgModelName;
                    d.InputModel.MemberName = readOnlyProp + (readOnlyPipe == null ? "" : " | " + readOnlyPipe);
                }
            }
            return d;
        }





        public override ControlGroupWriter TextAreaGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, IValidationCollection coll, int size, string alternateLabel, string placeHolder, object attrs, bool localizable, object inputAttr, string inputClasses, string groupClasses)
        {
            var d = base.TextAreaGroup(helper, exp, coll, size, alternateLabel, placeHolder, attrs, localizable, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, InputControls.CalendarTextBox);
            return d;
        }

        public override ControlGroupWriter TextBoxGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string textType, IValidationCollection coll, int size, string alternateLabel, string placeHolder, bool localizable, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var d = base.TextBoxGroup(helper, exp, textType, coll, size, alternateLabel, placeHolder, localizable, attrs, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, InputControls.TextBox);
            return d;
        }

        public override ControlGroupWriter LabelGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, int size, string filter, string alternateLabel, string url, bool blank, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var d = base.LabelGroup(helper, exp, size, filter, alternateLabel, url, blank, attrs, inputAttr, inputClasses, groupClasses);
            d.Accessibility = proc.Process(helper, exp, InputControls.Label);
            return d;
        }

        public override IHtmlContent ListView<T>(IHtmlHelper<T> helper, Expression<Func<T, IEnumerable>> exp, string listName, string display, bool mutli, string change)
        {
            var d = base.ListView(helper, exp, listName, display, mutli, change);
            var acc = proc.Process(helper, exp, "ListView");
            if (!acc.Read)
                return null;
            return d;
        }

        public override IHtmlContent ListView<T>(IHtmlHelper<T> helper, string selectionSource, string dataSource, string display, bool mutli, string onChange)
        {
            var d = base.ListView(helper, selectionSource, dataSource, display, mutli, onChange);
            var acc = proc.Process(helper, selectionSource.ToLower(), "ListView");
            if (!acc.Read)
                return null;
            return d;
        }

        public override IHtmlContent InputControl<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, InputControls component, string textType, Dictionary<string, object> radioOptions, int size, string alternateLabel, string placeHolder, IValidationCollection coll, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            ComponentWriter mod = GetInputControlWriter(helper, exp, component, textType, radioOptions, size, alternateLabel, placeHolder, coll, attrs, inputAttr, inputClasses, groupClasses);
            mod.Accessibility = proc.Process(helper, exp, component);
            if (!mod.Accessibility.Read)
                return null;
            if (!mod.Accessibility.Write)
                return mod.GetLabelControl();
            return mod.GetInputControl(component);
        }

        public override ControlGroupWriter SelectInputControl<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister lister, string display, string valueMember, bool required, string readOnlyProperty, bool nullable, object attrs, string inputClasses, string readOnlyPipe, string idExtra)
        {
            var mod = base.SelectInputControl(helper, exp, lister, display, valueMember, required, readOnlyProperty, nullable, attrs, inputClasses, readOnlyPipe, idExtra);
            mod.Accessibility = proc.Process(helper, exp, InputControls.Select);

            if (mod.Accessibility.Read)
            {
                lister.CollectionIdentifier = helper.GetCollectionName(RazorUtils.GetIdentifier(exp));
                helper.AddSource(lister);
            }


            if (!mod.Accessibility.Write)
            {
                if (readOnlyProperty != null)
                {
                    mod.InputModel.NgModelName = (readOnlyPipe == "translate" ? "'Words.'+" : "") + mod.InputModel.NgModelName;
                    mod.InputModel.MemberName = readOnlyProperty + (readOnlyPipe == null ? "" : " | " + readOnlyPipe);
                }
            }

            return mod;
        }

        public override ControlGroupWriter SearchableControlGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string display, string valueMember, bool multi, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses, string readOnlyProperty, bool nullable)
        {
            var mod = base.SearchableControlGroup(helper, exp, source, display, valueMember, multi, required, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses, readOnlyProperty, nullable);
            mod.Accessibility = proc.Process(helper, exp, InputControls.CalendarTextBox);

            if (mod.Accessibility.Read)
            {
                source.CollectionIdentifier = helper.GetCollectionName(RazorUtils.GetIdentifier(exp));
                helper.AddSource(source);
            }


            if (!mod.Accessibility.Write)
            {
                if (readOnlyProperty != null)
                {
                    // mod.InputModel.NgModelName = (readOnlyPipe == "translate" ? "'Words.'+" : "") + mod.InputModel.NgModelName;
                    mod.InputModel.MemberName = readOnlyProperty;
                }
            }
            return mod;
        }

        public override ControlGroupWriter SelectControlGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string display, string valueMember, bool multi, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses, string idExtra, string readOnlyProperty, string readOnlyPipe, bool nullable)
        {

            var mod = base.SelectControlGroup(helper, exp, source, display, valueMember, multi, required, size, alternateLabel, attrs, inputAttr, inputClasses, groupClasses, idExtra, readOnlyProperty, readOnlyPipe, nullable);
            mod.Accessibility = proc.Process(helper, exp, InputControls.Select, idExtra);

            if (mod.Accessibility.Read)
            {
                source.CollectionIdentifier = helper.GetCollectionName(RazorUtils.GetIdentifier(exp));
                helper.AddSource(source);
            }


            if (!mod.Accessibility.Write)
            {
                if (readOnlyProperty != null)
                {
                    mod.InputModel.NgModelName = (readOnlyPipe == "translate" ? "'Words.'+" : "") + mod.InputModel.NgModelName;
                    mod.InputModel.MemberName = readOnlyProperty + (readOnlyPipe == null ? "" : " | " + readOnlyPipe);
                }
            }

            return mod;

        }




    }
}
