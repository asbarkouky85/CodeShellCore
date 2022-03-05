using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using CodeShellCore.Moldster;
using CodeShellCore.Web.Razor.Validation;
using CodeShellCore.Moldster.CodeGeneration;

namespace CodeShellCore.Web.Razor.Tables
{
    public class DefaultTablesHelper : ITablesHelper
    {
        public virtual CellWriter CalendarCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes rangeType, Calendars cals, DateRange range, bool required, object cellAttributes, object inputAttr, string classes)
        {
            var writer = new CellWriter(helper);
            writer.UseExpression(exp);

            writer.Initialize(null, null, cellAttributes, inputAttr, classes);


            return writer;
        }

        public virtual CellWriter DragContentCell<T>(IHtmlHelper<T> helper, string tableName, string modelName, string width, object cellAttributes)
        {
            var writer = new CellWriter(helper);

            writer.Initialize(null, null, cellAttributes, null);
            writer.InputModel.NgModelName = modelName ?? helper.GetModelName();

            return writer;
        }

        public virtual CellWriter DragHeaderCell<T>(IHtmlHelper<T> helper, string tableName, string width, object cellAttributes)
        {
            var writer = new CellWriter(helper);
            writer.Initialize(null, width, cellAttributes, null);
            writer.InputModel.PlaceHolder = RazorConfig.LocaleTextProvider.Word("Drag");
            return writer;
        }

        public virtual CellWriter HeaderCell(IHtmlHelper helper, string textId, string size, bool isColumn, object cellAttributes, bool sorting, bool isRequired)
        {
            using (var writer = new CellWriter(helper))
            {
                writer.ColumnModel.MemberName = textId;
                writer.ColumnModel.Sorting = sorting;
                writer.ColumnModel.IsRequired = isRequired;
                writer.ColumnModel.Attributes = cellAttributes == null ? "" : RazorUtils.ToAttributeString(cellAttributes);
                writer.InputModel.PlaceHolder = isColumn ? RazorConfig.LocaleTextProvider.Column(textId) : RazorConfig.LocaleTextProvider.Word(textId);
                return writer;
            }
        }

        public virtual CellWriter HeaderCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string size, object cellAttributes, bool sorting, bool isRequired)
        {
            using (var writer = new CellWriter(helper))
            {
                writer.UseExpression(exp);
                writer.Initialize(null, size, cellAttributes, null);
                writer.ColumnModel.Sorting = sorting;
                writer.ColumnModel.IsRequired = isRequired;
                writer.InputModel.PlaceHolder = RazorConfig.LocaleTextProvider.Column(writer.ColumnId);
                return writer;
            }
        }

        public virtual IHtmlContent HeaderRow(IHtmlHelper helper, params string[] cols)
        {
            throw new NotImplementedException();
        }

        public CellWriter LinkCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string url, bool blank, object cellAttributes, object linkAttributes, string pipe)
        {
            var writer = new CellWriter(helper);
            writer.UseExpression(exp);
            writer.Initialize(null, null, cellAttributes, null);
            //if (url != null)
            //    url = RazorUtils.ApplyConvension(url, AppParts.Route);
            writer.InputModel = writer.InputModel.GetLabelInput(pipe, url, blank);
            pipe = pipe == null ? "" : " | " + pipe;
            if (pipe.Contains("translate"))
            {
                writer.InputModel.NgModelName = "'Words.'+" + writer.InputModel.NgModelName;
            }

            writer.InputModel.MemberName += pipe;
            return writer;
        }

        public virtual CellWriter SelectCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string displayMember, string valueMember, bool required, bool multi, object cellAttributes, object inputAttr, string classes, bool nullable, string rowIndex)
        {
            var writer = new CellWriter(helper);
            writer.UseExpression(exp);
            writer.Initialize(null, null, cellAttributes, inputAttr, classes);
            writer.InputModel = writer.InputModel.GetSelectInput(source.IsLookup ? "Lookups." + source.ListName : source.ListName, displayMember, valueMember, multi, nullable);
            if (required)
                writer.UseValidation(helper.VCollection().AddRequired(), rowIndex: rowIndex);
            return writer;
        }

        public virtual CellWriter SelectorCellMulti<T>(IHtmlHelper<T> helper, string field, string rowIndex, string listName, string ngModel, object cellAttributes, object inputAttr, string listItem, string classes)
        {
            var writer = new CellWriter(helper);

            //writer.AddToControls("SelectorCell");

            //if (!writer.GetAccessibility().Write)
            //    return new HtmlString("");

            string lItem = helper.GetModelName() + "." + (listItem ?? "Tag");
            writer.Initialize(null, null, cellAttributes, inputAttr, classes);
            writer.InputModel = writer.InputModel.GetCheckInput(null, null, false, lItem);
            writer.InputModel.MemberName = helper.GetModelName() + "." + ngModel;
            writer.InputModel.FieldName = "'" + field + "'+" + rowIndex;

            writer.InputModelExtraAttrs.evnt__change = helper.GetModelName() + $".Tag.ApplyTo({listName})";

            return writer;
        }

        public virtual CellWriter TextBoxCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string textBoxType, string rowIndex, IValidationCollection coll, object cellAttributes, object inputAttr, string classes)
        {
            var writer = new CellWriter(helper);
            writer.UseExpression(exp);

            writer.InputModel.TextBoxType = textBoxType;
            writer.InputModel.RowIndex = rowIndex;
            writer.Initialize(null, null, cellAttributes, inputAttr, classes);
            if (coll != null)
                writer.UseValidation(coll, rowIndex: rowIndex);
            return writer;
        }

        public virtual CellWriter TextCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, object cellAttributes)
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
