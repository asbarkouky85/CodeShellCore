using CodeShellCore.Web.Razor.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Web.Razor.Tables.Angular
{
    public class AngularTablesHelper : DefaultTablesHelper, IAngularTablesHelper
    {
        public virtual CellWriter CheckBoxCell<T>(IHtmlHelper<T> helper, string field, string rowIndex, string listName, string ngModel, object cellAttributes, object inputAttr, string listItem, string classes)
        {
            var writer = new CellWriter(helper);

            string lItem = helper.GetModelName() + "." + (listItem ?? "Tag");
            writer.Initialize(null, null, cellAttributes, inputAttr, classes);
            writer.InputModel = writer.InputModel.GetCheckInput(null, null, false, lItem);
            writer.InputModel.MemberName = helper.GetModelName() + "." + ngModel;
            writer.InputModel.FieldName = "'" + field + "'+" + rowIndex;

            writer.InputModelExtraAttrs.evnt__change = helper.GetModelName() + $".Tag.ApplyTo({listName})";

            return writer;
        }

        public virtual IHtmlContent ListModifiers<T>(IHtmlHelper<T> helper, string idExpression, IEnumerable<LinkModel> buttons, string detailsFunction, string editFunction, string deleteFunction, string identifier, string modifiers, string permissionName, string classes)
        {
            ListModifiersModel mod = new ListModifiersModel
            {
                ModelExpression = helper.GetModelName(),
                IdExpression = idExpression ?? helper.GetModelName() + ".id",
                DeleteFunction = deleteFunction,
                DetailsFunction = detailsFunction,
                EditFunction = editFunction,
                ReadOnly = false,
                Modifiers = modifiers,
                Classes = classes,
                PermissionVariable = permissionName
            };
            identifier = identifier?.ToLower();

            if (buttons != null)
            {
                mod.AdditionalButtons = buttons;
                foreach (var b in mod.AdditionalButtons)
                    b.Classes = "buttonGra-sm";
            }

            return helper.GetComponent("TableCells/ListModifiers", mod);
        }

        public virtual CellWriter RadioBoxCell<T>(IHtmlHelper<T> helper,string field, string rowIndex, string property, bool asArray, object cellAttributes, object inputAttr, string listItem, string classes)
        {
            var writer = new CellWriter(helper);

            writer.Initialize(null, null, cellAttributes, inputAttr, classes);

            string lItem = helper.GetModelName() + (listItem != null ? "." + listItem : null);
            string type = "checkbox";
            if (asArray)
            {
                writer.InputModel.MemberName = writer.InputModel.NgModelName + ".Tag.selected";
            }
            else
            {
                type = "radio";
                writer.InputModel.MemberName = writer.InputModel.NgModelName + "." + property;
                writer.InputModelExtraAttrs.calc__value = rowIndex;
            }

            writer.InputModel = writer.InputModel.GetCheckInput(null, null, false, lItem, type);
            writer.InputModel.FieldName = "'" + field + "'";

            if (asArray)
                writer.InputModelExtraAttrs.evnt__change = helper.GetModelName() + $".Tag.SelectOnly({property})";
            return writer;
        }

        public CellWriter TextCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string pipe, object cellAttributes)
        {
            using (var writer = new CellWriter(helper))
            {
                writer.UseExpression(exp);
                writer.Initialize(null, null, cellAttributes, null);

                pipe = pipe == null ? "" : " | " + pipe;
                if (pipe.Contains("translate"))
                {
                    writer.InputModel.NgModelName = "'Words.'+" + writer.InputModel.NgModelName;
                }

                writer.InputModel.MemberName += pipe;
                return writer;
            }
        }
    }
}
