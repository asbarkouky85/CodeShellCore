using CodeShellCore.Data.Recursion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public static class LookupsExtensions
    {
        public static IEnumerable<T> Recurse<T>(this IEnumerable<T> items, long? startAt = null) where T : class, IRecursiveModel
        {
            IEnumerable<T> lst = items.OrderBy(d=>d.Name).Where(d => d.ParentId == startAt);
            foreach (var s in lst)
                s.AppendChildren(items);
            
            return lst.OrderByDescending(d => d.Children.Any()).ThenBy(d => d.Name);
        }

        public static void AppendChildren<T>(this T item, IEnumerable<T> all) where T : class, IRecursiveModel
        {
            item.Children = all.Where(d => d.ParentId == item.Id);
            foreach (var s in item.Children)
                s.AppendChildren(all);
            item.Children = item.Children.OrderByDescending(d => d.Children.Any()).ThenBy(d=>d.Name);
        }

    }
}
