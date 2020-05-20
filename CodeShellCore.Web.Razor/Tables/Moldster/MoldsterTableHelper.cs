using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CodeShellCore.Moldster;
using CodeShellCore.Web.Razor.Containers;
using CodeShellCore.Web.Razor.General.Moldster;
using CodeShellCore.Web.Razor.Models;
using CodeShellCore.Web.Razor.Tables.Angular;
using CodeShellCore.Web.Razor.Validation;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeShellCore.Web.Razor.Tables.Moldster
{
    public class MoldsterTableHelper : AngularTablesHelper, IMoldsterTableHelper
    {
        private IdentifierProcessor proc = new IdentifierProcessor();

        public MoldsterTableHelper()
        {
        }

        public override CellWriter TextCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, object cellAttributes)
        {
            var wt = base.TextCell(helper, exp, cellAttributes);
            wt.Accessibility = proc.ProcessCell(helper, exp, InputControls.Label);
            return wt;
        }

        public CellWriter AutoCompleteSelectCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string displayMember, string valueMember, MoldsterHtmlContainer parent, bool required, bool multi, bool nullable, object cellAttributes, object inputAttr, string classes)
        {
            var writer = base.SelectCell(helper, exp, source, displayMember, valueMember, required, multi, cellAttributes, inputAttr, classes, false,null);
            writer.ColumnModel.Attributes = RazorUtils.ToAttributeString(cellAttributes);
            ((SelectNgInput)writer.InputModel).Nullable = nullable;
            writer.Accessibility = proc.ProcessCell(helper, exp, InputControls.Select, parent);
            writer.Initialize(null, null, cellAttributes, inputAttr, classes);
            return writer;
        }

        public override CellWriter CalendarCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, CalendarTypes rangeType, Calendars cals, DateRange range, bool required, object cellAttributes, object inputAttr, string classes)
        {
            var wt = base.CalendarCell(helper, exp, rangeType, cals, range, required, cellAttributes, inputAttr, classes);
            wt.Accessibility = proc.ProcessCell(helper, exp, InputControls.CalendarTextBox);
            return wt;
        }

        public CellWriter CalendarCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, MoldsterHtmlContainer parent, CalendarTypes rangeType, Calendars cals, DateRange range, bool required, object cellAttributes, object inputAttr, string classes)
        {
            var wt = base.CalendarCell(helper, exp, rangeType, cals, range, required, cellAttributes, inputAttr, classes);
            wt.Accessibility = proc.ProcessCell(helper, exp, InputControls.CalendarTextBox, parent);
            return wt;
        }

        public override CellWriter CheckBoxCell<T>(IHtmlHelper<T> helper, string field, string rowIndex, string listName, string ngModel,string changeFunction, object cellAttributes, object inputAttr, string listItem, string classes)
        {
            var wt = base.CheckBoxCell(helper, field, rowIndex, listName, ngModel, changeFunction, cellAttributes, inputAttr, listItem, classes);
            wt.Accessibility = proc.ProcessCell(helper, field, InputControls.CheckBoxCell);
            return wt;
        }

        public override CellWriter HeaderCell(IHtmlHelper helper, string textId, string size, bool isColumn, object cellAttributes, bool sorting)
        {
            return base.HeaderCell(helper, textId, size, isColumn, cellAttributes, sorting);
        }

        public override CellWriter HeaderCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string size, object cellAttributes, bool sorting)
        {
            var s = base.HeaderCell(helper, exp, size, cellAttributes, sorting);
            s.Accessibility = proc.ProcessCell(helper, exp, "Header");
            return s;
        }

        public CellWriter HeaderCell<T>(IHtmlHelper<T> helper, MoldsterHtmlContainer parent, string textId, bool isColumn = false, string size = null, object cellAttributes = null, bool ignoreAccessibility = false, bool sorting = true)
        {
            var wt = base.HeaderCell(helper, textId, size, isColumn, cellAttributes, sorting);
            if (!ignoreAccessibility)
                wt.Accessibility = proc.ProcessCell(helper, textId, "Header", parent);
            return wt;
        }

        public CellWriter HeaderCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, MoldsterHtmlContainer parent, string width, bool ignoreAccessibility, object cellAttributes, bool sorting)
        {
            var wt = base.HeaderCell<T, TValue>(helper, exp, width, cellAttributes, sorting);
            if (!ignoreAccessibility)
                wt.Accessibility = proc.ProcessCell(helper, exp, "Header", parent);
            return wt;
        }



        public override IHtmlContent ListModifiers<T>(IHtmlHelper<T> helper, string idExpression, IEnumerable<LinkModel> buttons, string detailsFunction, string editFunction, string deleteFunction, string identifier, string modifiers, string permissionName, string classes)
        {
            var item = proc.Process(helper, (identifier ?? "list_modifiers"), "list_modifiers");
            if (!item.Read)
                return null;
            return base.ListModifiers(helper, idExpression, buttons, detailsFunction, editFunction, deleteFunction, identifier, modifiers, permissionName, classes);
        }

        public override CellWriter RadioBoxCell<T>(IHtmlHelper<T> helper, string field, string rowIndex, string property, bool asArray,string changeFunction, object cellAttributes, object inputAttr, string listItem, string classes)
        {
            var wt = base.RadioBoxCell(helper, field, rowIndex, property, asArray,changeFunction, cellAttributes, inputAttr, listItem, classes);
            wt.Accessibility = proc.ProcessCell(helper, field, InputControls.CheckBoxCell);

            return wt;
        }

        public override CellWriter DragHeaderCell<T>(IHtmlHelper<T> helper, string tableName, string width, object cellAttributes)
        {
            var wt = base.DragHeaderCell(helper, tableName, width, cellAttributes);
            wt.Accessibility = proc.ProcessCell(helper, tableName + "__moving", "DragCell");
            return wt;
        }

        public override CellWriter DragContentCell<T>(IHtmlHelper<T> helper, string tableName, string modelName, string width, object cellAttributes)
        {
            var wt = base.DragContentCell(helper, tableName, modelName, width, cellAttributes);
            wt.Accessibility = proc.ProcessCell(helper, tableName + "__moving", "DragCell");
            return wt;
        }

        public override CellWriter SelectCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, Lister source, string displayMember, string valueMember, bool required, bool multi, object cellAttributes, object inputAttr, string classes, bool nullable, string rowIndex)
        {
            var wt = base.SelectCell(helper, exp, source, displayMember, valueMember, required, multi, cellAttributes, inputAttr, classes, nullable, rowIndex);
            wt.Accessibility = proc.ProcessCell(helper, exp, InputControls.Select);
            helper.AddSource(source);
            return wt;
        }

        public CellWriter SelectCell<T, TValue>(
            IHtmlHelper<T> helper,
            Expression<Func<T, TValue>> exp,
            Lister source,
            string displayMember,
            string valueMember,
            MoldsterHtmlContainer parent,
            bool required,
            bool multi,
            bool nullable,
            object cellAttributes,
            object inputAttr,
            string classes,
            string readOnlyProp,
            string rowIndex)
        {
            var writer = base.SelectCell(helper, exp, source, displayMember, valueMember, required, multi, cellAttributes, inputAttr, classes, nullable,rowIndex);
            writer.ColumnModel.Attributes = RazorUtils.ToAttributeString(cellAttributes);
            ((SelectNgInput)writer.InputModel).Nullable = nullable;
            writer.Accessibility = proc.ProcessCell(helper, exp, InputControls.Select, parent);
            writer.Initialize(null, null, cellAttributes, inputAttr, classes);

            helper.AddSource(source);
            if (!writer.Accessibility.Write)
            {
                if (readOnlyProp != null)
                    writer.InputModel.MemberName = readOnlyProp;
            }
            writer.InputModel.RowIndex = rowIndex;
            return writer;
        }

        public CellWriter TextBoxCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, MoldsterHtmlContainer parent, string textBoxType, IValidationCollection coll, object cellAttributes, object inputAttr, string classes, string rowIndex, bool localizable)
        {
            var w = base.TextBoxCell(helper, exp, textBoxType, rowIndex, coll, cellAttributes, inputAttr, classes);
            w.Accessibility = proc.ProcessCell(helper, exp, InputControls.TextBox, parent);
            return w;
        }

        public override CellWriter TextBoxCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string textBoxType, string rowIndex, IValidationCollection coll, object cellAttributes, object inputAttr, string classes)
        {
            var w = base.TextBoxCell(helper, exp, textBoxType, rowIndex, coll, cellAttributes, inputAttr, classes);
            w.Accessibility = proc.ProcessCell(helper, exp, InputControls.TextBox);
            return w;
        }



        public CellWriter TextCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, MoldsterHtmlContainer parent, string pipe, object cellAttributes)
        {
            var w = base.TextCell(helper, exp, pipe, cellAttributes);
            w.Accessibility = proc.ProcessCell(helper, exp, InputControls.Label, parent);
            return w;
        }

        public override CellWriter TextCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string pipe, object cellAttributes)
        {
            var w = base.TextCell(helper, exp, pipe, cellAttributes);
            w.Accessibility = proc.ProcessCell(helper, exp, InputControls.Label);
            return w;
        }


    }
}
