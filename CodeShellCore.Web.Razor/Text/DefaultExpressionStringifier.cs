using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Web.Razor.Text
{
    public class DefaultExpressionStringifier : IExpressionStringifier
    {
        public string GetColumnId(MemberExpression exp,string memberName=null)
        {

            if (exp.Expression is MemberExpression)
            {
                MemberExpression inExp = exp.Expression as MemberExpression;
                if (inExp.Member.Name == "Entity")
                    return exp.Member.Name;
                return GetMemberName(inExp) + "." + exp.Member.Name;
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
                return GetMemberName(inExp) + "." + exp.Member.Name;
            }
            else
            {
                return exp.Member.Name;
            }
        }
    }
}
