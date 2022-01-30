using CodeShellCore.Data.Helpers;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Data
{
    public interface IAdapterRepository<TChild, T> : IRepository<TChild> where T : class where TChild : class, T
    {
        T GetInstance();
        TValue IGetValue<TValue>(object id, Expression<Func<T, TValue>> ex);
        TValue IGetSingleValue<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter);
        IEnumerable<TValue> IGetValues<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter = null);
        IEnumerable<TValue> IGetValues<TValue, TOrder>(Expression<Func<T, TValue>> ex, Expression<Func<T, TOrder>> order, Expression<Func<T, bool>> filter = null);

        T IFindSingle(object id);
        T IFindSingle(Expression<Func<T, bool>> expression);
        TR IFindSingleAs<TR>(Expression<Func<T, TR>> exp, object id) where TR : class;
        TR IFindSingleAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> expression) where TR : class;

        void IAdd(T obj);
        void IUpdate(T obj);
        void IDelete(T obj);
        void IMerge(T obj);

        List<T> IGetList();
        List<T> IFind(Expression<Func<T, bool>> exp);
        LoadResult IFind(ListOptions<T> opts);

        List<TR> IFindAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> cond = null, ListOptions<TR> opts = null) where TR : class;
        LoadResult IFindAs<TR>(Expression<Func<T, TR>> exp, ListOptions<TR> opts, Expression<Func<T, bool>> cond = null) where TR : class;

        int ICount(Expression<Func<T, bool>> exp);

        bool IExist(Expression<Func<T, bool>> exp);
    }
}