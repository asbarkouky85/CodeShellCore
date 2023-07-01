using CodeShellCore.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Linq.Filtering
{

    /// <summary>
    /// Used to create <see cref="PropertyFilter"/> object with the correct data
    /// </summary>
    public class PropertyFilter<T, TValue> : PropertyFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public PropertyFilter(Expression<Func<T, TValue>> ex)
        {
            MemberExpression exp = (MemberExpression)ex.Body;

            MemberName = exp.Member.Name;
            Type t = exp.Type;
            if (t == typeof(string))
                FilterType = "string";
            else if (t.IsDecimalType())
                FilterType = "decimal";
            else if (t.IsIntgerType())
                FilterType = "int";
            else if (t == typeof(DateTime))
                FilterType = "date";
            Ids = new List<string>();
        }
    }
}
