using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Linq
{
    public enum SortDir { ASC, DESC }
    /// <summary>
    /// Object defining conditions for <see cref="IRepository.Find(LoadOptions)"/>
    /// </summary>
    public class ListOptions<T> : ListOptions where T : class
    {
        /// <summary>
        /// List of Expression&lt;Func&lt;T,bool&gt;&gt;
        /// </summary>
        /// <example>
        /// <code>param => param.ID == 3</code>
        /// </example>
        public List<Expression> Filters { get; set; }

        
        /// <summary>
        /// Object defining conditions for <see cref="IRepository.Find(ListOptions)"/>
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

        public ListOptions<TTarget> Convert<TTarget>() where TTarget : class
        {
            ListOptions<TTarget> t = new ListOptions<TTarget>();
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
