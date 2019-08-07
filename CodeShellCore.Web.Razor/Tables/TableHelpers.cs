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

    public static class TableHelpers
    {
        static ITablesHelper Provider { get { return Shell.ScopedInjector.GetRequiredService<ITablesHelper>(); } }

        public static IHtmlContent HeaderRow(this IHtmlHelper helper, params string[] parameters)
        {
            return Provider.HeaderRow(helper, parameters);
        }

        public static IHtmlContent HeaderCell(this IHtmlHelper helper,
            string text,
            string size = null,
            bool isColumn = true,
            object cellAttributes = null)
        {
            CellWriter mod = Provider.HeaderCell(helper, text, size, isColumn, cellAttributes);
            return mod.WriteHeaderCell();
        }
        public static IHtmlContent HeaderCell<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            string size = null,
            object cellAttributes = null)
        {
            CellWriter mod = Provider.HeaderCell(helper, exp, size, cellAttributes);
            return mod.WriteHeaderCell();
        }


        public static IHtmlContent TextCell<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            object cellAttributes = null)
        {
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
            string classes = "")
        {
            CellWriter mod = Provider.SelectCell(helper, exp, source, displayMember, valueMember, required, multi, cellAttributes, inputAttr, classes);
            return mod.Write(InputControls.Select);
        }

        public static IHtmlContent TextBoxCell<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            string textBoxType = "text",
           IValidationCollection coll = null,
            object cellAttributes = null,
            object inputAttr = null,
            string classes = "")
        {
            CellWriter mod = Provider.TextBoxCell(helper, exp, textBoxType, coll, cellAttributes, inputAttr, classes);
            return mod.Write(InputControls.TextBox);
        }

        public static IHtmlContent CalendarCell<T, TValue>(this IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,
            CalendarTypes rangeType = CalendarTypes.PastAndFuture,
            Calendars cals = Calendars.Greg,
            DateRange range = null,
            bool required = false,
            object cellAttributes = null,
            object inputAttr = null,
            string classes = "")
        {
            CellWriter mod = Provider.CalendarCell(helper, exp, rangeType, cals, range, required, cellAttributes, inputAttr, classes);
            return mod.Write(InputControls.CalendarTextBox);
        }



    }
}