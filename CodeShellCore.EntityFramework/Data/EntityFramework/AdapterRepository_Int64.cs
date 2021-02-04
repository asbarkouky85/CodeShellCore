using CodeShellCore.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CodeShellCore.Data.EntityFramework
{
    public class AdapterRepository_Int64<TModel, T, TContext> : Repository_Int64<TModel, TContext>, IAdapterRepository<TModel, T>
        where TModel : class, T, IModel<long>
        where T : class
        where TContext : DbContext
    {
        protected IQueryable<T> ILoader { get { return Loader as IQueryable<T>; } }

        public AdapterRepository_Int64(TContext con) : base(con)
        {
        }


        public List<T> IGetList()
        {
            return ILoader.ToList();
        }

        public virtual void IAdd(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("entity");
            Saver.Add((TModel)obj);
        }

        public virtual void IDelete(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("entity");
            DbContext.Entry(obj).State = EntityState.Deleted;
        }

        public virtual void IUpdate(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("entity");
            DbContext.Entry(obj).State = EntityState.Modified;
        }

        public virtual int ICount(Expression<Func<T, bool>> exp)
        {
            return Loader.Where(exp).Count(d => true);
        }

        public virtual bool IExist(Expression<Func<T, bool>> exp)
        {
            return Loader.Any(exp);
        }

        public virtual List<T> Find(Expression<Func<T, bool>> exp)
        {
            return Loader.Where(exp).ToList();
        }

        public virtual LoadResult IFind(ListOptions<T> opts)
        {
            return ILoader.LoadWith(opts);
        }

        public virtual List<TR> IFindAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> cond = null, ListOptions<TR> opts = null) where TR : class
        {
            var q = ILoader;
            if (cond != null)
                q = q.Where(cond);

            if (opts != null)
                return q.Select(exp).ToListWith(opts);

            return q.Select(exp).ToList();
        }

        public virtual LoadResult IFindAs<TR>(Expression<Func<T, TR>> exp, ListOptions<TR> opts, Expression<Func<T, bool>> cond = null) where TR : class
        {
            var q = ILoader;
            if (cond != null)
                q = q.Where(cond);

            return q.Select(exp).LoadWith(opts);
        }

        public virtual T IFindSingle(Expression<Func<T, bool>> expression)
        {
            return Loader.Where(expression).FirstOrDefault();
        }

        public virtual TR IFindSingleAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> expression) where TR : class
        {
            return Loader.Where(expression).Select(exp).FirstOrDefault();
        }

        public virtual IEnumerable<TValue> IGetValues<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter)
        {
            var q = ILoader;
            if (filter != null)
                q = q.Where(filter);
            return q.Select(ex);
        }

        public virtual TValue IGetSingleValue<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter)
        {
            return Loader.Where(filter).Select(ex).FirstOrDefault();
        }

        public IEnumerable<TValue> IGetValues<TValue, TOrder>(Expression<Func<T, TValue>> ex, Expression<Func<T, TOrder>> order, Expression<Func<T, bool>> filter = null)
        {
            var q = ILoader;
            if (filter != null)
                q = q.Where(filter);
            return q.OrderBy(order).Select(ex).ToList();
        }

        public List<T> IFind(Expression<Func<T, bool>> filter)
        {
            var q = ILoader;
            if (filter != null)
                q = q.Where(filter);
            return q.ToList();
        }

        public T GetInstance()
        {
            return Activator.CreateInstance<TModel>();
        }

        public TValue IGetValue<TValue>(object id, Expression<Func<T, TValue>> ex)
        {
            return Loader.Where(d => d.Id == (long)id).Select(ex).FirstOrDefault();
        }

        public T IFindSingle(object id)
        {
            return FindSingle(id);
        }

        public TR IFindSingleAs<TR>(Expression<Func<T, TR>> exp, object id) where TR : class
        {
            return Loader.Where(d => d.Id == (long)id).Select(exp).FirstOrDefault();
        }

        public void IMerge(T obj)
        {
            Merge((TModel)obj);
        }
    }
}
