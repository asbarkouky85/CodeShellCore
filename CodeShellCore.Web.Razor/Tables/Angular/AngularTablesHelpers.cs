using CodeShellCore.Web.Razor.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Tables.Angular
{
    public static class AngularTablesHelpers
    {
        public static IHtmlContent ListModifiers<T>(this IHtmlHelper<T> helper,
            string idExpression = null,
            IEnumerable<LinkModel> buttons = null,
            string detailsFunction = null,
            string editFunction = null,
            string deleteFunction = null,
            string identifier = "ListModifiers",
            string modifiers = "DER",
            string permissionName = "Permission",
            string classes = "")
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
    }
}
