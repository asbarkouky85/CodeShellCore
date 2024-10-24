using CodeShellCore.Data;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Linq.Filtering;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore
{
    public static class AppUtils
    {
        public static List<Expression<Func<T, bool>>> GetFilters<T>(IEnumerable<PropertyFilterDto> fs) where T : class
        {
            return Expressions.ToStrongExpressions<T>(fs);
        }

        public static List<Named<object>> GetNamedList<T>(string prefix = "")
        {
            var lst = new List<Named<object>>();
            foreach (Enum cond in Enum.GetValues(typeof(T)))
            {
                lst.Add(new Named<object>
                {
                    Id = Convert.ToInt32(cond),
                    Name = prefix + EnumExtensions.GetString(cond)
                });
            }
            return lst;
        }
    }
}
