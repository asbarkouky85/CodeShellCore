using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

using CodeShellCore.Web.Razor.Models;
using CodeShellCore.Web.Razor.Validation;
using CodeShellCore.Helpers;
using CodeShellCore.Text;
using CodeShellCore.Web.Razor.General.Moldster;
using CodeShellCore.Moldster.Dto;

namespace CodeShellCore.Web.Razor.Elements.Angular
{
    public class AngularElementsHelper : DefaultElementsHelper, IAngularElementsHelper
    {
        public IHtmlContent InputControl_Ng<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, InputControls component, string textType, Dictionary<string, object> radioOptions, IValidationCollection coll, int size, string alternateLabel, string placeHolder, object attrs, string listItem, string inputClasses)
        {
            var mod = GetInputControlWriter(helper, exp, component, textType, radioOptions, size, alternateLabel, placeHolder, coll, null, attrs, inputClasses, null);
            if (component == InputControls.CheckBox)
                ((CheckNgInput)mod.InputModel).ListItemName = listItem ?? helper.GetModelName();

            return mod.Write(component);
        }

        public override ControlGroupWriter LabelGroup<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, int size, string filter, string alternateLabel, string url, bool blank, object attrs, object inputAttr, string inputClasses, string groupClasses)
        {
            var mod = base.LabelGroup(helper, exp, size, filter, alternateLabel, url, blank, attrs, inputAttr, inputClasses, groupClasses);
            if (filter == "translate")
                mod.InputModel.NgModelName = "'Words.'+" + mod.InputModel.NgModelName;
            return mod;
        }

        public override IHtmlContent ListView<T>(IHtmlHelper<T> helper, Expression<Func<T, IEnumerable>> exp, string listName, string display, bool mutli, string change)
        {
            var mod = GetListViewWriter(helper, exp, listName, display, mutli, change);
            var sel = mod.InputModel.GetSelectInput(listName, display, null, mutli);

            if (change == "Default")
            {
                sel.ChangeFunction = mutli ? "item.Tag.ApplyTo(" + mod.InputModel.NgModelName + "." + mod.InputModel.MemberName + ")" : "item.Tag.SelectOnly(" + mod.InputModel.NgModelName + "." + mod.InputModel.MemberName + ")";
            }
            else if (change != null)
            {
                sel.ChangeFunction = change;
            }
            mod.InputModel = sel;
            return mod.Write("Components/ListView");
        }

        public override IHtmlContent ListView<T>(IHtmlHelper<T> helper, string selectionSource, string dataSource, string display, bool mutli, string onChange)
        {
            var mod = GetListViewWriter(helper, selectionSource, dataSource, display, mutli, onChange);
            var sel = mod.InputModel.GetSelectInput(dataSource, display, null, mutli);

            if (onChange == "Default")
            {
                sel.ChangeFunction = mutli ? "item.Tag.ApplyTo(" + mod.InputModel.NgModelName + "." + mod.InputModel.MemberName + ")" : "item.Tag.SelectOnly(" + mod.InputModel.NgModelName + "." + mod.InputModel.MemberName + ")";
            }
            else if (onChange != null)
            {
                sel.ChangeFunction = onChange;
            }
            mod.InputModel = sel;
            return mod.Write("Components/ListView");
        }

        public virtual IHtmlContent SelectModalButton<T>(IHtmlHelper<T> helper,
            string textId,
            string function,
            string bind,
            object attrs,
            string identifier,
            string validationFunction,
            string url,
           IEnumerable<LinkModel> lst = null)
        {
            var mod = new PopupOpenerModel
            {
                Text = textId.StartsWith("col__") ? helper.Column(textId.GetAfterFirst("__")) : helper.Word(textId),
                Function = function,
                Content = bind,
                Attrs = attrs,
                GroupName = identifier ?? textId.Replace(" ", "_"),
                ValidationFunction = validationFunction,
                Required = validationFunction != null,
                Url = url,
                Buttons = lst
            };

            if (mod.Required)
            {
                var coll = helper.VCollection().AddCustom("required_modal", helper.Message("value_is_required").ToString());
                mod.FormFieldName = "field__" + Utils.GenerateID();
                coll.SetMember("x", mod.FormFieldName);
                mod.ValidationMessage = coll.GetMessages(helper);
            }



            return helper.GetComponent("Components/ModalSelectButton", mod);
        }

        public IHtmlContent ValueBinding<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> ex, string pipe)
        {
            string content = helper.GetModelName() + "." + RazorUtils.GetMemberName(ex);
            if (pipe == "translate")
            {
                content = "'Words.'+" + content;
            }
            pipe = pipe == null ? "" : " | " + pipe;
            return new HtmlString("{{" + content + pipe + "}}");
        }
    }
}
