using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Linq
{
    public static class ApplicationLinqExstensions
    {
        

        public static bool HasChanges<T>(this IEnumerable<T> lst) where T : class, IEditable
        {
            return lst.Any(d => d.State == "Added" || d.State == "Modified" || d.State == "Deleted");
        }

        public static IEnumerable<T> GetAdded<T>(this IEnumerable<T> lst) where T : class, IEditable
        {
            return lst.Where(d => d.State == "Added");
        }

        public static IEnumerable<T> GetModified<T>(this IEnumerable<T> lst) where T : class, IEditable
        {
            return lst.Where(d => d.State == "Modified");
        }
    }
}
