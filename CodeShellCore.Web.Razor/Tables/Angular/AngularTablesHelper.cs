
using CodeShellCore.Web.Razor.Tables;
using CodeShellCore.Web.Razor.Tables.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Web.Razor.Tables.Internal
{
    public class AngularTablesHelper : DefaultTablesHelper
    {
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
    }
}
