﻿using CodeShellCore.Moldster;
using CodeShellCore.Web.Razor.Containers;
using CodeShellCore.Web.Razor.Models;
using CodeShellCore.Web.Razor.Validation;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Web.Razor.Tables.Moldster
{
    public static class MoldsterTableExtensions
    {
        static IMoldsterTableHelper Provider { get { return Shell.ScopedInjector.GetRequiredService<IMoldsterTableHelper>(); } }

        public static IHtmlContent HeaderCell<T>(this IHtmlHelper<T> helper,
            MoldsterHtmlContainer parent,
            string textId,
            bool isColumn = false,
            string size = null,
            object cellAttributes = null,
            bool ignoreAccessibility = false,
            bool? sorting = null)
        {
            sorting = sorting ?? helper.GetTheme().SortingInTables;
            var writer = Provider.HeaderCell(helper, parent, textId, isColumn, size, cellAttributes, ignoreAccessibility);
            return writer.WriteHeaderCell();
        }

        public static IHtmlContent HeaderCell<T, TValue>(this IHtmlHelper<T> helper,
            Expression<Func<T, TValue>> exp,
            MoldsterHtmlContainer parent = null,
            string width = null,
            bool ignoreAccessibility = false,
            object cellAttributes = null,
            bool? sorting = null)
        {
            sorting = sorting ?? helper.GetTheme().SortingInTables;
            CellWriter wt = Provider.HeaderCell(helper, exp, parent, width, ignoreAccessibility, cellAttributes);
            return wt.WriteHeaderCell();
        }

        public static IHtmlContent TextCell<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            MoldsterHtmlContainer parent = null,
            string modelName = null,
            object cellAttributes = null,
            string pipe = null)
        {
            var writer = Provider.TextCell(helper, exp, parent, pipe, cellAttributes);
            return writer.WriteCell(CellTypes.LabelCell);
        }

        public static IHtmlContent SelectCell<T, TValue>(this IHtmlHelper<T> helper,
            Expression<Func<T, TValue>> exp,
            Lister source,
            string displayMember,
            string valueMember,
            MoldsterHtmlContainer parent = null,
            bool required = false,
            bool multi = false,
            bool nullable = false,
            object cellAttributes = null,
            object inputAttr = null,
            string classes = "",
            string readOnlyProp = null,
            string rowIndex = null)
        {
            var writer = Provider.SelectCell(helper, exp, source, displayMember, valueMember, parent, required, multi, nullable, cellAttributes, inputAttr, classes, readOnlyProp, rowIndex);

            return writer.Write(InputControls.Select);
        }

        public static IHtmlContent AutoCompleteSelectCell<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,

            Lister source,
            string displayMember,
            string valueMember = null,
            MoldsterHtmlContainer parent = null,
            bool required = false,
            bool multi = false,
            bool nullable = false,
            object cellAttributes = null,
            object inputAttr = null,
            string classes = "")
        {
            CellWriter wt = Provider.AutoCompleteSelectCell(helper, exp, source, displayMember, valueMember, parent, required, multi, nullable, cellAttributes, inputAttr, classes);
            return wt.Write(InputControls.Select_Searchable);
        }

        public static IHtmlContent TextBoxCell<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            MoldsterHtmlContainer parent = null,
            string textBoxType = "text",
            IValidationCollection coll = null,
            object cellAttributes = null,
            object inputAttr = null,
            string classes = "",
            string rowIndex = null,
            bool localizable = false)
        {
            CellWriter wt = Provider.TextBoxCell(helper, exp, parent, textBoxType, coll, cellAttributes, inputAttr, classes, rowIndex, localizable);
            return wt.Write(InputControls.TextBox);
        }



        public static IHtmlContent CalendarCell<T, TValue>(
            IHtmlHelper<T> helper,
            Expression<Func<T, TValue>> exp,
            MoldsterHtmlContainer parent,
            CalendarTypes rangeType,
            Calendars cals,
            DateRange range,
            bool required,
            object cellAttributes,
            object inputAttr,
            string classes)
        {
            CellWriter wt = Provider.CalendarCell(helper, exp, parent, rangeType, cals, range, required, cellAttributes, inputAttr, classes);
            return wt.Write(InputControls.CalendarTextBox);
        }

        public static IHtmlContent DragHeaderCell<T>(this IHtmlHelper<T> helper,
            string tableName,
            string width = null,
            object cellAttributes = null)
        {
            CellWriter writer = Provider.DragHeaderCell<T>(helper, tableName, width, cellAttributes);
            return writer.WriteHeaderCell();
        }

        public static IHtmlContent DragContentCell<T>(this IHtmlHelper<T> helper,
            string tableName,
            string modelName = null,
            string width = null,
            object cellAttributes = null)
        {
            CellWriter writer = Provider.DragContentCell<T>(helper, tableName, modelName, width, cellAttributes);
            return writer.WriteCell(CellTypes.DragCell);
        }

        public static IHtmlContent LinkCell<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            string url,
            bool blank = true,
            object cellAttributes = null,
            object linkAttributes = null,
            string pipe = null)
        {
            CellWriter writer = Provider.LinkCell<T, TValue>(helper, exp, url, blank, cellAttributes, linkAttributes, pipe);
            return writer.Write(InputControls.Label);
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
            return Provider.ListModifiers<T>(helper, idExpression, buttons, detailsFunction, editFunction, deleteFunction, identifier, modifiers, permissionName, classes);

        }

        public static IHtmlContent CheckBoxCell<T>(
            this IHtmlHelper<T> helper,
            string field,
            string rowIndex,
            string listName,
            string ngModel = "Tag.selected",
            string changeFunction = null,
            object cellAttributes = null,
            object inputAttr = null,
            string listItem = "Tag",
            string classes = "")
        {
            CellWriter mod = Provider.CheckBoxCell(helper, field, rowIndex, listName, ngModel, changeFunction, cellAttributes, inputAttr, listItem, classes);
            return mod.Write(InputControls.CheckBoxCell);
        }

        public static IHtmlContent RadioBoxCell<T>(this IHtmlHelper<T> helper,
            string field,
            string rowIndex,
            string property,
            bool asArray = true,
            string changeFunction = null,
            object cellAttributes = null,
            object inputAttr = null,
            string listItem = "Tag",
            string classes = "")
        {
            CellWriter mod = Provider.RadioBoxCell(helper, field, rowIndex, property, asArray, changeFunction, cellAttributes, inputAttr, listItem, classes);
            return mod.Write(InputControls.CheckBoxCell);
        }
    }
}