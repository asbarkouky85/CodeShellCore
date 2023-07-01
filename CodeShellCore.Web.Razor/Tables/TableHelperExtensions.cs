using System;
using System.Linq.Expressions;
using CodeShellCore.Moldster;
using CodeShellCore.Web.Razor.Validation;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Web.Razor.Tables
{

    public static class TableHelperExtensions
    {
        //static ITablesHelper Provider { get { return Shell.ScopedInjector.GetRequiredService<ITablesHelper>(); } }

        public static IHtmlContent HeaderRow(this IHtmlHelper helper, params string[] parameters)
        {
            var Provider = helper.GetService<ITablesHelper>();
            return Provider.HeaderRow(helper, parameters);
        }

        public static IHtmlContent HeaderCell(this IHtmlHelper helper,
            string text,
            string size = null,
            bool isColumn = true,
            object cellAttributes = null,
            bool? sorting = null,
            bool isRequired = false)
        {
            var Provider = helper.GetService<ITablesHelper>();
            sorting = sorting ?? helper.GetTheme().SortingInTables;
            CellWriter mod = Provider.HeaderCell(helper, text, size, isColumn, cellAttributes, sorting.Value, isRequired);
            return mod.WriteHeaderCell();
        }
        public static IHtmlContent HeaderCell<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            string size = null,

            object cellAttributes = null,
            bool? sorting = null,
            bool isRequired = false)
        {
            var Provider = helper.GetService<ITablesHelper>();
            sorting = sorting ?? helper.GetTheme().SortingInTables;
            CellWriter mod = Provider.HeaderCell(helper, exp, size, cellAttributes, sorting.Value, isRequired);
            return mod.WriteHeaderCell();
        }


        public static IHtmlContent TextCell<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            object cellAttributes = null)
        {
            var Provider = helper.GetService<ITablesHelper>();
            CellWriter mod = Provider.TextCell(helper, exp, cellAttributes);
            return mod.WriteCell(CellTypes.LabelCell);
        }

        public static IHtmlContent SelectCell<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            Lister source,
            string displayMember,
            string valueMember = null,
            bool required = false,
            bool multi = false,
            object cellAttributes = null,
            object inputAttr = null,
            string classes = "",
            bool nullable = false,
            string rowIndex = null)
        {
            var Provider = helper.GetService<ITablesHelper>();
            CellWriter mod = Provider.SelectCell(helper, exp, source, displayMember, valueMember, required, multi, cellAttributes, inputAttr, classes, nullable, rowIndex);
            return mod.Write(InputControls.Select);
        }

        public static IHtmlContent TextBoxCell<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            string textBoxType = "text",
           IValidationCollection coll = null,
            object cellAttributes = null,
            object inputAttr = null,
            string classes = "")
        {
            var Provider = helper.GetService<ITablesHelper>();
            CellWriter mod = Provider.TextBoxCell(helper, exp, textBoxType, null, coll, cellAttributes, inputAttr, classes);
            return mod.Write(InputControls.TextBox);
        }

        public static IHtmlContent CalendarCell<T, TValue>(
            this IHtmlHelper<T> helper,
            Expression<Func<T, TValue>> exp,
            CalendarTypes rangeType = CalendarTypes.PastAndFuture,
            Calendars cals = Calendars.Greg,
            DateRange range = null,
            bool required = false,
            object cellAttributes = null,
            object inputAttr = null,
            string classes = "")
        {
            var Provider = helper.GetService<ITablesHelper>();
            CellWriter mod = Provider.CalendarCell(helper, exp, rangeType, cals, range, required, cellAttributes, inputAttr, classes);
            return mod.Write(InputControls.CalendarTextBox);
        }

        public static IHtmlContent DragHeaderCell<T>(this IHtmlHelper<T> helper,
            string tableName,
            string width = null,
            object cellAttributes = null)
        {
            var Provider = helper.GetService<ITablesHelper>();
            CellWriter writer = Provider.DragHeaderCell<T>(helper, tableName, width, cellAttributes);
            return writer.WriteHeaderCell();
        }

        public static IHtmlContent DragContentCell<T>(this IHtmlHelper<T> helper,
            string tableName,
            string modelName = null,
            string width = null,
            object cellAttributes = null)
        {
            var Provider = helper.GetService<ITablesHelper>();
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
            var Provider = helper.GetService<ITablesHelper>();
            CellWriter writer = Provider.LinkCell(helper, exp, url, blank, cellAttributes, linkAttributes, pipe);
            return writer.Write(InputControls.Label);
        }


    }
}
