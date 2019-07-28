using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CodeShellCore.Moldster;
using CodeShellCore.Web.Razor.Validation;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.Tables
{
    public class DefaultTablesHelper : ITablesHelper
    {
        public CellWriter CalendarCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes rangeType, Calendars cals, DateRange range, bool required, object cellAttributes, object inputAttr, string classes)
        {


            throw new NotImplementedException();
        }

        public CellWriter HeaderCell(IHtmlHelper helper, string textId, string size, bool isColumn, object cellAttributes)
        {
            using (var writer = new CellWriter(helper))
            {
                writer.ColumnModel.Attributes = cellAttributes == null ? "" : RazorUtils.ToAttributeString(cellAttributes);
                writer.InputModel.PlaceHolder = isColumn ? RazorConfig.LocaleTextProvider.Column(textId) : RazorConfig.LocaleTextProvider.Word(textId);
                return writer;
            }
        }

        public CellWriter HeaderCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string size, object cellAttributes)
        {
            using (var writer = new CellWriter(helper))
            {
                writer.UseExpression(exp);
                writer.Initialize(null, size, cellAttributes, null);
                writer.InputModel.PlaceHolder = RazorConfig.LocaleTextProvider.Column(writer.ColumnId);
                return writer;
            }
        }

        public IHtmlContent HeaderRow(IHtmlHelper helper, params string[] cols)
        {
            throw new NotImplementedException();
        }

        public CellWriter SelectCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string displayMember, string valueMember, bool required, bool multi, object cellAttributes, object inputAttr, string classes)
        {
            var writer = new CellWriter(helper);
            writer.UseExpression(exp);
            writer.Initialize(null, null, cellAttributes, inputAttr, classes);
            writer.InputModel = writer.InputModel.GetSelectInput(source.IsLookup ? "Lookups." + source.ListName : source.ListName, displayMember, valueMember);
            if (required)
                writer.UseValidation(helper.VCollection().AddRequired());
            return writer;
        }

        public CellWriter TextBoxCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string textBoxType, IValidationCollection coll, object cellAttributes, object inputAttr, string classes)
        {
            throw new NotImplementedException();
        }

        public CellWriter TextCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, object cellAttributes)
        {
            using (var writer = new CellWriter(helper))
            {
                writer.UseExpression(exp);
                writer.Initialize(null, null, cellAttributes, null);
                return writer;
            }
        }
    }
}
