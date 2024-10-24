using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Linq
{
    public enum SortDir { ASC, DESC }
    /// <summary>
    /// Object defining conditions for <see cref="IRepository.Find(PagedListRequestDto)"/>
    /// </summary>
    public class PagedListRequest<T> : PagedListRequest where T : class
    {
        /// <summary>
        /// List of Expression&lt;Func&lt;T,bool&gt;&gt;
        /// </summary>
        /// <example>
        /// <code>param => param.ID == 3</code>
        /// </example>
        public List<Expression> Filters { get; set; }

        public void SetOrderProperty<TVal>(Expression<Func<T, TVal>> ex, SortDir dir = SortDir.ASC)
        {
            OrderProperty = ((MemberExpression)ex.Body).Member.Name;
            Direction = dir;
        }
        /// <summary>
        /// Object defining conditions for <see cref="IRepository.Find(PagedListRequest)"/>
        /// </summary>
        /// <summary>
        /// To facilitate the creation of filter expressions according to the type of {T}
        /// </summary>
        /// <param name="expression"></param>
        public void AddFilter(Expression<Func<T, bool>> expression)
        {
            if (Filters == null)
                Filters = new List<Expression>();
            Filters.Add(expression);
        }

        public void AddFilter<TInterface>(Expression<Func<TInterface, bool>> expression)
        {
            if (Filters == null)
                Filters = new List<Expression>();
            Filters.Add(expression);
        }

        public PagedListRequest<TTarget> Convert<TTarget>() where TTarget : class
        {
            PagedListRequest<TTarget> t = new PagedListRequest<TTarget>();
            t.Filters = new List<Expression>();
            t.AppendProperties(this);
            return t;
        }

        public List<Expression<Func<T, bool>>> GetExpressions()
        {
            if (Filters == null)
                return null;
            var fils = new List<Expression<Func<T, bool>>>();
            foreach (var f in Filters)
            {
                var ef = (Expression<Func<T, bool>>)f;
                if (ef != null)
                    fils.Add(ef);
            }
            return fils;

        }
    }
}
