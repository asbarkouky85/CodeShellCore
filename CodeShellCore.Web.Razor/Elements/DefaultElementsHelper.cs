﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodeShellCore.Web.Razor.Validation;
using Microsoft.AspNetCore.Html;

namespace CodeShellCore.Web.Razor.Elements
{
    public class DefaultElementsHelper : IElementsHelper
    {
        public virtual ControlGroupWriter AutoCompleteGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister src, bool required, int size, string alternateLabel, string placeHolder, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = new ControlGroupWriter(helper);

            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);
            IValidationCollection coll = null;
            if (required)
                coll = helper.VCollection().AddRequired();
            mod.UseValidation(coll, mod.InputModel.FieldName, alternateLabel);

            mod.InputModel.TextBoxType = "text";
            mod.InputModel.NgOptions = src.IsLookup ? "Lookups." + src.ListName : src.ListName;

            return mod;

        }

        public virtual ControlGroupWriter CalendarGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes type, DateRange range, Calendars cals, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = new ControlGroupWriter(helper);
            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, null, attrs, inputAttr, inputClasses, groupClasses);

            if (!mod.Accessibility.Write)
            {
                mod.InputModel.MemberName = mod.InputModel.MemberName + " | date :'dd-MM-yyyy'";
                return mod;
            }

            IValidationCollection coll = helper.GetDateValidators(type, required, range, mod.InputModel.MemberName, alternateLabel);
            mod.UseValidation(coll, mod.InputModel.FieldName, alternateLabel);

            return mod;

        }

        public virtual ControlGroupWriter CheckBoxGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string trueString, string falseString, bool useIcon, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = new ControlGroupWriter(helper);
            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, null, attrs, inputAttr, inputClasses, groupClasses);
            mod.InputModel = mod.InputModel.GetCheckInput(helper.Word(trueString), helper.Word(falseString), useIcon);
            return mod;
        }

        public virtual ControlGroupWriter DateTimeGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes type, DateRange range, Calendars cals, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = new ControlGroupWriter(helper);
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

        public virtual ControlGroupWriter FileGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string formFieldName, string uploadUrl, IValidationCollection coll, int size, string alternateLabel, string placeHolder, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = new ControlGroupWriter(helper);
            mod.UseExpression(exp);
            mod.InputModel = mod.InputModel.GetFileInput(uploadUrl, formFieldName);
            mod.SetOptions(size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);
            mod.UseValidation(coll, formFieldName, alternateLabel);

            return mod;

        }

        protected ComponentWriter GetInputControlWriter<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, InputControls component, string textType, Dictionary<string, object> radioOptions, int size, string alternateLabel, string placeHolder, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = new ControlGroupWriter(helper);

            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);

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

        public virtual IHtmlContent InputControl<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, InputControls component, string textType, Dictionary<string, object> radioOptions, int size, string alternateLabel, string placeHolder, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            ComponentWriter mod = GetInputControlWriter(helper, exp, component, textType, radioOptions, size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);
            return mod.Write(component);
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
            var mod = new ControlGroupWriter(helper);
            mod.UseExpression(exp);
            mod.InputModel = mod.InputModel.GetLabelInput(filter, url, blank);
            mod.SetOptions(size, alternateLabel, null, attrs, inputAttr, inputClasses, groupClasses);
            return mod;

        }


        protected ComponentWriter GetListViewWriter<T>(IHtmlHelper<T> helper, Expression<Func<T, IEnumerable>> exp, string listName, string display, bool mutli, string change)
        {
            var mod = new ControlGroupWriter(helper);
            mod.UseExpression(exp);
            var sel = mod.InputModel.GetSelectInput(listName, display, null, mutli);
            sel.Display = display;
            mod.InputModel = sel;
            return mod;
        }

        protected ComponentWriter GetListViewWriter<T>(IHtmlHelper<T> helper, string selectionSource,string dataSource, string display, bool mutli, string change)
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

        public virtual ControlGroupWriter RadioGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Dictionary<string, object> options, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses, string template)
        {
            var mod = new ControlGroupWriter(helper);

            mod.UseExpression(exp);
            mod.InputModel = mod.InputModel.GetRadioInput(options);
            mod.SetOptions(size, alternateLabel, null, attrs, inputAttr, inputClasses, groupClasses);

            return mod;

        }

        public virtual ControlGroupWriter SearchableControlGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string display, string valueMember, bool multi, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses, bool nullable)
        {
            var mod = new ControlGroupWriter(helper);

            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, null, attrs, inputAttr, inputClasses, groupClasses);
            mod.InputModel = mod.InputModel.GetSelectInput((source.IsLookup ? "Lookups." : "") + source.ListName, display, valueMember, false, nullable);

            if (required)
                mod.UseValidation(helper.VCollection().AddRequired(), mod.InputModel.FieldName, alternateLabel);

            return mod;

        }

        public virtual ControlGroupWriter SelectControlGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string display, string valueMember, bool multi, bool required, int size, string alternateLabel, object attrs, object inputAttr, string inputClasses, string groupClasses, bool nullable)
        {
            var mod = new ControlGroupWriter(helper);

            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, null, attrs, inputAttr, inputClasses, groupClasses);
            mod.InputModel = mod.InputModel.GetSelectInput((source.IsLookup ? "Lookups." : "") + source.ListName, display, valueMember, false, nullable);

            if (required)
                mod.UseValidation(helper.VCollection().AddRequired(), mod.InputModel.FieldName, alternateLabel);

            return mod;

        }

        public virtual ControlGroupWriter TextAreaGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, IValidationCollection coll, int size, string alternateLabel, string placeHolder, object attrs, bool localizable, object inputAttr, string inputClasses, string groupClasses) where T : class
        {
            var mod = new ControlGroupWriter(helper);

            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);
            mod.UseValidation(coll, mod.InputModel.FieldName, alternateLabel);

            mod.GroupModel.IsTextArea = true;

            return mod;

        }

        public virtual ControlGroupWriter TextBoxGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string textType, IValidationCollection coll, int size, string alternateLabel, string placeHolder, bool localizable, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = new ControlGroupWriter(helper);

            mod.UseExpression(exp);
            mod.SetOptions(size, alternateLabel, placeHolder, attrs, inputAttr, inputClasses, groupClasses);
            mod.UseValidation(coll, mod.InputModel.FieldName, alternateLabel);

            mod.InputModel.TextBoxType = textType;

            return mod;

        }
    }
}
