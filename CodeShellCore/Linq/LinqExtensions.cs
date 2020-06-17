using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq.Filtering;
using CodeShellCore.MQ;

namespace CodeShellCore.Linq
{
    public static class LinqExtensions
    {
        public static IEnumerable<TR> MapList<T, TR>(this List<T> lst) where T : class where TR : class
        {
            Expression<Func<T, TR>> expression = ExpressionStore.GetExpression<T, TR>();
            return lst.Select(expression.Compile()).ToList();
        }

        public static IEnumerable<TR> MapList<T, TR>(this IQueryable<T> lst) where T : class where TR : class
        {
            return lst.Select(ExpressionStore.GetExpression<T, TR>()).ToList();
        }

        public static IEnumerable<TR> MapList<T, TR>(this IEnumerable<T> lst) where T : class where TR : class
        {
            Expression<Func<T, TR>> expression = ExpressionStore.GetExpression<T, TR>();
            if (lst is IQueryable<T>)
                ((IQueryable<T>)lst).Select(expression).ToList();
            return lst.Select(expression.Compile()).ToList();
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> lst, Action<T> func)
        {
            foreach (T item in lst)
                func(item);
            return lst;
        }

        public static List<Named<TPrime>> GetNamedList<T, TPrime>(this IRepository<T> repo, Expression<Func<T, TPrime>> expression) where T : class, INamed<TPrime>
        {
            return repo.FindAs(e => new Named<TPrime> { Id = e.Id, Name = e.Name }).OrderBy(d => d.Name).ToList();
        }



        public static ChangeSet<T> ApplyChanges<T>(this IRepository<T> repo, IEnumerable<T> lst) where T : class, IEditable
        {
            ChangeSet<T> set = ChangeSet.Create(lst);
            set.Apply(repo);
            if (set.Added.Count() == 0 && set.Updated.Count() == 0 && set.Deleted.Count() == 0)
                set = null;
            return set;
        }

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

        public static List<T> MapTo<T>(this IEnumerable lst, bool ignoreId = true, IEnumerable<string> ignore = null) where T : class
        {
            List<T> t = new List<T>();
            foreach (var ob in lst)
            {
                t.Add(ob.MapTo<T>(ignoreId, ignore));
            }
            return t;
        }

        public static T MapTo<T>(this object obj, bool ignoreId = true, IEnumerable<string> ignore = null) where T : class
        {
            T inst = Activator.CreateInstance<T>();
            inst.AppendProperties(obj, ignoreId, ignore);
            return inst;
        }

        public static void PresistState<T, TPrime>(this IEnumerable<DTO<T, TPrime>> lst)
            where T : class, IEditable
        {
            lst.ForEach(d => d.Entity.State = d.State);
        }

        public static object Copy(this ISharedModel obj)
        {
            object ob = Activator.CreateInstance(obj.GetType());
            ob.AppendProperties(obj, false, obj.GetNavPropertyNames());
            return ob;
        }

        public static void AppendProperties(this object model, object ob, bool ignoreId = true, IEnumerable<string> ignore = null)
        {
            ignore = ignore == null ? new List<string>() : ignore;
            PropertyInfo[] props = ob.GetType().GetProperties();
            Dictionary<string, PropertyInfo> modelProps = model.GetType()
                .GetProperties()
                .Where(
                    d => (d.Name != "Id" || !ignoreId) &&
                    (d.PropertyType == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(d.PropertyType)) &&
                    !ignore.Contains(d.Name) &&
                    d.CanWrite
                )
                .ToDictionary(d => d.Name);

            foreach (PropertyInfo inf in props)
            {
                if (modelProps.ContainsKey(inf.Name))
                {
                    object v = inf.GetValue(ob);
                    modelProps[inf.Name].SetValue(model, v);
                }
            }
        }
        public static Expression<Func<T, bool>> Combine<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> exp2)
        {
            ParameterExpression para = Expression.Parameter(typeof(T));
            BinaryExpression combinedExpression = Expression.MakeBinary(ExpressionType.And, exp, exp2);
            return Expression.Lambda<Func<T, bool>>(combinedExpression, para);
        }

        public static List<T> ToListWith<T>(this IQueryable<T> q, FilterCollection coll) where T : class
        {
            var fils = coll.GetFiltersFor<T>();
            foreach (var ex in fils)
                q = q.Where(ex);
            return q.ToList();
        }

        public static IQueryable<T> SortWith<T, TVal>(this IQueryable<T> q, Expression<Func<T, TVal>> exp, SortDir dir) where T : class
        {
            return (dir == SortDir.ASC) ? q.OrderBy(exp) : q.OrderByDescending(exp);
        }

        public static int Count<T>(this IQueryable<T> q, FilterCollection coll) where T : class
        {
            var fils = coll.GetFiltersFor<T>();
            foreach (var ex in fils)
                q = q.Where(ex);
            return q.Count(e => true);
        }

        public static List<T> ToListWith<T>(this IQueryable<T> q, ListOptions<T> opts) where T : class
        {
            ExpressionGenerator<T> gen = new ExpressionGenerator<T>();

            if (opts.Filters != null)
            {
                for (int i = 0; i < opts.Filters.Count; i++)
                {
                    Expression<Func<T, bool>> e = (Expression<Func<T, bool>>)opts.Filters[i];
                    q = q.Where(e);
                }
            }

            if (!string.IsNullOrEmpty(opts.OrderProperty))
                q = gen.SortWith(q, opts.OrderProperty, opts.Direction);

            if (opts.Showing > 0)
                q = q.Skip(opts.Skip).Take(opts.Showing);

            return q.ToList();

        }

        public static LoadResult<T> LoadWith<T>(this IQueryable<T> q, ListOptions<T> opts) where T : class
        {
            LoadResult<T> res = new LoadResult<T>();
            
            ExpressionGenerator<T> gen = new ExpressionGenerator<T>();

            if (opts == null)
                opts = new ListOptions<T>();

            if (opts.Filters != null)
            {
                for (int i = 0; i < opts.Filters.Count; i++)
                {
                    Expression<Func<T, bool>> e = (Expression<Func<T, bool>>)opts.Filters[i];
                    q = q.Where(e);
                }
               
                    res.TotalCount = q.Count(v => true);
            }
            else
            {
                res.TotalCount = q.Count(d => true);
            }


            if (!string.IsNullOrEmpty(opts.OrderProperty))
                q = gen.SortWith(q, opts.OrderProperty, opts.Direction);

            if (opts.Showing > 0)
                q = q.Skip(opts.Skip).Take(opts.Showing);

            res.List = q.ToList();
            return res;
        }
    }
}
