using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq.Expressions;

namespace CodeShellCore.Web.Razor.Tables.Angular
{
    public class AngularTablesHelper : DefaultTablesHelper, IAngularTablesHelper
    {
        public CellWriter CheckBoxCell(IHtmlHelper helper, string field, string rowIndex, string listName, string ngModel, object cellAttributes, object inputAttr, string listItem, string classes)
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

        public CellWriter RadioBoxCell(IHtmlHelper helper,string field, string rowIndex, string property, bool asArray, object cellAttributes, object inputAttr, string listItem, string classes)
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

        public CellWriter TextCell<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp,string pipe, object cellAttributes)
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

        public CellWriter TextCell_Angular<T, TValue>(IHtmlHelper<T> helper, Expression<Func<T, TValue>> exp, string pipe, object cellAttributes)
        {
            throw new NotImplementedException();
        }
    }
}
