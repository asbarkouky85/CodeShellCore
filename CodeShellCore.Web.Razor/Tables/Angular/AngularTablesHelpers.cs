using CodeShellCore.Web.Razor.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace CodeShellCore.Web.Razor.Tables.Angular
{
    public static class AngularTablesHelpers
    {
        static IAngularTablesHelper Provider { get { return Shell.ScopedInjector.GetRequiredService<IAngularTablesHelper>(); } }

        public static IHtmlContent CheckBoxCell<T>(this IHtmlHelper<T> helper,
            string field,
            string rowIndex,
            string listName,
            string ngModel = "Tag.selected",
            object cellAttributes = null,
            object inputAttr = null,
            string listItem = "Tag",
            string classes = "")
        {
            CellWriter mod = Provider.CheckBoxCell(helper, field, rowIndex, listName, ngModel, cellAttributes, inputAttr, listItem, classes);
            return mod.Write(InputControls.CheckBoxCell);
        }

        public static IHtmlContent RadioBoxCell<T>(this IHtmlHelper<T> helper,
            string field,
            string rowIndex,
            string property,
            bool asArray = true,
            object cellAttributes = null,
            object inputAttr = null,
            string listItem = "Tag",
            string classes = "")
        {
            CellWriter mod = Provider.RadioBoxCell(helper,field, rowIndex, property, asArray, cellAttributes, inputAttr, listItem, classes);
            return mod.Write(InputControls.CheckBoxCell);
        }

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
            return Provider.ListModifiers<T>(helper,idExpression,buttons,detailsFunction,editFunction,deleteFunction,identifier,modifiers,permissionName,classes);
            
        }
    }
}
