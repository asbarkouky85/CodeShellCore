using CodeShellCore.Text;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Web.Razor.Text
{
    public class AngularExpressionStringifier : IExpressionStringifier
    {
        public string GetColumnId(MemberExpression exp, string memberName = null)
        {
            if (exp.Expression is MemberExpression)
            {
                MemberExpression inExp = exp.Expression as MemberExpression;
                string mem = memberName == null ? "." + exp.Member.Name : "";
                return inExp.Type.Name+"." + exp.Member.Name;
            }
            else
            {
                return exp.Member.Name;
            }
        }

        public string GetMemberName(MemberExpression exp)
        {
            if (exp.Expression is MemberExpression)
            {
                MemberExpression inExp = exp.Expression as MemberExpression;
                return GetMemberName(inExp).LCFirst() + "." + exp.Member.Name.LCFirst();
            }
            else
            {
                return exp.Member.Name.LCFirst() ;
            }
        }
    }
}
