using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Linq.Stringifiers
{
    public class DefaultExpressionStringifier : IExpressionStringifier
    {
        public virtual string GetColumnId(MemberExpression exp, string memberName = null)
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

        public virtual string GetMemberName(MemberExpression exp)
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

        public virtual string GetOriginalPropertyName(MemberExpression body)
        {

            return body.Member.Name;

        }
    }
}
