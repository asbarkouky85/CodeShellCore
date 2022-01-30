using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodeShellCore.Web.Razor.Validation;
using Microsoft.AspNetCore.Html;
using CodeShellCore.Moldster;
using System.Linq;

namespace CodeShellCore.Web.Razor.Elements
{
    public class DefaultElementsHelper : IElementsHelper
    {
        public virtual ControlGroupWriter GetNewWriter(IHtmlHelper h)
        {
            return new ControlGroupWriter(h);
        }
        public virtual ControlGroupWriter AutoCompleteGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister src, IValidationCollection coll, int size, string alternateLabel, string placeHolder, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = GetNewWriter(helper);

            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);

            if (coll != null)
                mod.UseValidation(coll, mod.InputModel.FieldName, alternateLabel);

            mod.InputModel.TextBoxType = "text";
            mod.InputModel.NgOptions = src.IsLookup ? "Lookups." + src.ListName : src.ListName;

            return mod;

        }

        public virtual ControlGroupWriter CalendarGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes type, DateRange range, Calendars cals, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = GetNewWriter(helper);
            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, null, attrs, inputAttr, inputClasses, groupClasses);

            IValidationCollection coll = helper.GetDateValidators(type, required, range, mod.InputModel.MemberName, alternateLabel);
            mod.UseValidation(coll, mod.InputModel.FieldName, alternateLabel);

            return mod;

        }

        public virtual ControlGroupWriter CheckBoxGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string trueString, string falseString, bool useIcon, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = GetNewWriter(helper);
            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, null, attrs, inputAttr, inputClasses, groupClasses);
            var tr = trueString == null ? null : helper.Word(trueString);
            var fal = falseString == null ? null : helper.Word(falseString);
            mod.InputModel = mod.InputModel.GetCheckInput(tr, fal, useIcon);
            return mod;
        }

        public virtual ControlGroupWriter DateTimeGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes type, DateRange range, Calendars cals, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = GetNewWriter(helper);
            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, null, attrs, inputAttr, inputClasses, groupClasses);

            if (!mod.Accessibility.Write)
            {
                mod.InputModel.MemberName = mod.InputModel.MemberName + " | date :'dd-MM-yyyy (hh:mm a)'";
                return mod;
            }

            IValidationCollection coll = helper.GetDateValidators(type, required, range, mod.InputModel.MemberName, alternateLabel);
            mod.UseValidation(coll, mod.InputModel.FieldName, alternateLabel);

            return mod;

        }

        public virtual ControlGroupWriter FileGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string formFieldName, string uploadUrl, bool required, bool multiple, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = GetNewWriter(helper);
            mod.UseExpression(exp);
            mod.InputModel = mod.InputModel.GetFileInput(uploadUrl, formFieldName, multiple);
            mod.SetOptions(size, alternateLabel, null, attrs, inputAttr, inputClasses, groupClasses);
            if (required)
                mod.UseValidation(helper.VCollection().AddRequired(), formFieldName, alternateLabel);

            return mod;

        }

        protected virtual ComponentWriter GetInputControlWriter<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, InputControls component, string textType, Dictionary<string, object> radioOptions, int size, string alternateLabel, string placeHolder, IValidationCollection coll, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = GetNewWriter(helper);

            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);
            if (coll != null)
                mod.UseValidation(coll, null, alternateLabel);
            switch (component)
            {
                case InputControls.Radio:
                    mod.InputModel = mod.InputModel.GetRadioInput(radioOptions);
                    break;
                case InputControls.CheckBox:
                    mod.InputModel = mod.InputModel.GetCheckInput(null, null, true);
                    break;
            }

            mod.InputModel.TextBoxType = textType;
            return mod;
        }

        public virtual IHtmlContent InputControl<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, InputControls component, string textType, Dictionary<string, object> radioOptions, int size, string alternateLabel, string placeHolder, IValidationCollection coll, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            ComponentWriter mod = GetInputControlWriter(helper, exp, component, textType, radioOptions, size, alternateLabel, placeHolder, coll, attrs, inputAttr, inputClasses, groupClasses);
            if (!mod.Accessibility.Read)
                return null;
            if (!mod.Accessibility.Write)
                return mod.GetLabelControl();
            return mod.GetInputControl(component);
        }

        public virtual ControlGroupWriter LabelGroup<T, TValue>(
            IHtmlHelper<T> helper,
            Expression<Func<T, TValue>> exp,
            int size,
            string filter,
            string alternateLabel,
            string url,
            bool blank,
            object attrs,
            object inputAttr,
            string inputClasses,
            string groupClasses)
        {
            var mod = GetNewWriter(helper);
            mod.UseExpression(exp);
            mod.InputModel = mod.InputModel.GetLabelInput(filter, url, blank);
            mod.SetOptions(size, alternateLabel, null, attrs, inputAttr, inputClasses, groupClasses);
            return mod;

        }


        protected ComponentWriter GetListViewWriter<T>(IHtmlHelper<T> helper, Expression<Func<T, IEnumerable>> exp, string listName, string display, bool mutli, string change)
        {
            var mod = new ComponentWriter(helper);
            mod.UseExpression(exp);
            var sel = mod.InputModel.GetSelectInput(listName, display, null, mutli);
            sel.Display = display;
            mod.InputModel = sel;
            return mod;
        }

        protected ComponentWriter GetListViewWriter<T>(IHtmlHelper<T> helper, string selectionSource, string dataSource, string display, bool mutli, string change)
        {
            var mod = new ComponentWriter(helper);
            mod.InputModel = new Models.NgInput
            {
                MemberName = selectionSource,
                NgModelName = helper.GetModelName(),
                NgFormName = helper.GetFormName()
            };
            var sel = mod.InputModel.GetSelectInput(dataSource, display, null, mutli);
            sel.Display = display;
            mod.InputModel = sel;
            return mod;
        }

        public virtual IHtmlContent ListView<T>(IHtmlHelper<T> helper, Expression<Func<T, IEnumerable>> exp, string listName, string display, bool mutli, string change)
        {
            ComponentWriter mod = GetListViewWriter(helper, exp, listName, display, mutli, change);
            return mod.Write("Components/ListView");
        }

        public virtual IHtmlContent ListView<T>(IHtmlHelper<T> helper, string selectionSource, string dataSource, string display, bool mutli, string onChange)
        {
            ComponentWriter mod = GetListViewWriter(helper, selectionSource, dataSource, display, mutli, onChange);
            return mod.Write("Components/ListView"); ;

        }

        public virtual ControlGroupWriter RadioGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Dictionary<string, object> options, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses, string template, string readonlyProperty, string readOnlyPipe)
        {
            var mod = GetNewWriter(helper);

            string[] keys = new string[options.Keys.Count];
            string[] keyWords = new[] { "null" };
            options.Keys.CopyTo(keys, 0);
            foreach (var s in keys)
            {

                if (!(options[s] is string) || keyWords.Contains(options[s].ToString().ToLower()))
                {
                    options[s] = options[s].ToString().ToLower();
                }
                else if (!(options[s] as string).StartsWith("'"))
                {
                    options[s] = $"'{options[s]}'";
                }
            }

            mod.UseExpression(exp);

            if (required)
                mod.UseValidation(helper.VCollection().AddRequired(), mod.InputModel.FieldName, alternateLabel);

            mod.InputModel = mod.InputModel.GetRadioInput(options);
            mod.SetOptions(size, alternateLabel, null, attrs, inputAttr, inputClasses, groupClasses);

            return mod;

        }

        public virtual ControlGroupWriter SearchableControlGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string display, string valueMember, bool multi, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses, string readOnlyProperty, bool nullable)
        {
            var mod = GetNewWriter(helper);

            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, null, attrs, inputAttr, inputClasses, groupClasses);
            mod.InputModel = mod.InputModel.GetSelectInput((source.IsLookup ? "Lookups." : "") + source.ListName, display, valueMember, multi, nullable);

            if (required)
                mod.UseValidation(helper.VCollection().AddRequired(), mod.InputModel.FieldName, alternateLabel);

            return mod;

        }

        public virtual ControlGroupWriter SelectControlGroup<T, TValue>(
            IHtmlHelper<T> helper,
            Expression<Func<T, TValue>> exp,
            Lister source,
            string display,
            string valueMember,
            bool multi,
            bool required,
            int size,
            string alternateLabel,
            object attrs,
            object inputAttr,
            string inputClasses,
            string groupClasses,
            string idExtra,
            string readOnlyProperty,
            string readOnlyPipe,
            bool nullable)
        {
            var mod = GetNewWriter(helper);

            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, null, attrs, inputAttr, inputClasses, groupClasses);
            mod.InputModel = mod.InputModel.GetSelectInput((source.IsLookup ? "Lookups." : "") + source.ListName, display, valueMember, false, nullable);

            if (required)
                mod.UseValidation(helper.VCollection().AddRequired(), mod.InputModel.FieldName, alternateLabel);

            return mod;

        }



        public virtual ControlGroupWriter TextAreaGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, IValidationCollection coll, int size, string alternateLabel, string placeHolder, object attrs, bool localizable, object inputAttr, string inputClasses, string groupClasses) where T : class
        {
            var mod = GetNewWriter(helper);

            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);
            mod.UseValidation(coll, mod.InputModel.FieldName, alternateLabel);

            mod.GroupModel.IsTextArea = true;

            return mod;

        }

        public virtual ControlGroupWriter TextBoxGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string textType, IValidationCollection coll, int size, string alternateLabel, string placeHolder, bool localizable, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = GetNewWriter(helper);

            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);
            mod.UseValidation(coll, mod.InputModel.FieldName, alternateLabel);

            mod.InputModel.TextBoxType = textType;

            return mod;

        }

        public virtual ControlGroupWriter CustomInputGroup<T, TValue>(
            IHtmlHelper<T> helper,
            Expression<Func<T, TValue>> exp,
            string componentName,
            bool required,
            int size,
            object attrs,
            object inputAttrs,
            string classes)
        {
            var wt = GetNewWriter(helper);
            wt.UseExpression(exp);
            wt.SetOptions(size, null, null, attrs, inputAttrs, classes);

            return wt;
        }

        public virtual ControlGroupWriter RichLabel<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, int size, object attrs, object inputAttrs, string classes)
        {
            ControlGroupWriter wt = GetNewWriter(helper);
            wt.UseExpression(exp);
            wt.SetOptions(size, null, null, attrs, inputAttrs, null, classes);
            return wt;
        }

        public virtual ControlGroupWriter RichTextBox<T, TValue>(IHtmlHelper<T> helper,
            Expression<Func<T, TValue>> expression,
            string modules = null,
            int size = 6,
            object attrs = null,
            object inputAttrs = null,
            string classes = null)
        {
            ControlGroupWriter wt = GetNewWriter(helper);
            wt.UseExpression(expression);
            wt.SetOptions(size, null, null, attrs, inputAttrs, null, classes);
            wt.InputModel.AngularJsConfig = modules;

            return wt;
        }

        public virtual ControlGroupWriter SelectInputControl<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister lister, string display, string valueMember, bool required, string readOnlyProperty, bool nullable, object attrs, string inputClasses, string readOnlyPipe, string idExtra)
        {
            var mod = GetNewWriter(helper);

            mod.UseExpression(exp);
            mod.SetOptions(1, null, null, attrs, attrs, inputClasses, null);
            mod.InputModel = mod.InputModel.GetSelectInput((lister.IsLookup ? "Lookups." : "") + lister.ListName, display, valueMember, false, nullable);

            if (required)
            {
                mod.UseValidation(helper.VCollection().AddRequired(), mod.InputModel.FieldName);
            }
            mod.InputModel.GroupName = null;
            return mod;
        }
    }
}
