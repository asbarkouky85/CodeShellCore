using CodeShellCore.Text.Localization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Web.Razor.Text
{
    public interface IExpressionStringifier
    {
        string GetColumnId(MemberExpression exp, string memberName = null);
        string GetMemberName(MemberExpression exp);


    }
}
