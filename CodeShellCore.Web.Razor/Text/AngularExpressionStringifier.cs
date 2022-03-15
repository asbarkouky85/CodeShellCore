using CodeShellCore.Text;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CodeShellCore.Types;

namespace CodeShellCore.Web.Razor.Text
{
    public class AngularExpressionStringifier : IExpressionStringifier
    {
        public virtual string GetColumnId(MemberExpression exp, string memberName = null)
        {
            if (exp.Expression is MemberExpression)
            {
                MemberExpression inExp = exp.Expression as MemberExpression;
                string mem = memberName == null ? "." + exp.Member.Name : "";
                return inExp.Type.GetEntityName()+"." + exp.Member.Name;
            }
            else
            {
                return exp.Member.Name;
            }
        }

        public virtual string GetMemberName(MemberExpression exp)
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
