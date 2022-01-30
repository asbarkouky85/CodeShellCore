using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Data
{
    public interface IRepository<T> : IRepository where T : class
    {
        TValue GetValue<TValue>(object id, Expression<Func<T, TValue>> ex);
        TValue GetSingleValue<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter);
        IEnumerable<TValue> GetValues<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter = null);
        IEnumerable<TValue> GetValues<TValue, TOrder>(Expression<Func<T, TValue>> ex, Expression<Func<T, TOrder>> order, Expression<Func<T, bool>> filter = null);

        IEnumerable<Named<object>> FindAsLookup(string collectionId, Expression<Func<T, bool>> ex);
        T FindSingle(object id);
        T FindSingle(Expression<Func<T, bool>> expression);
        /// <summary>
        /// Returns true if exists
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="obj"></param>
        /// <param name="existing"></param>
        /// <returns></returns>
        bool FindSingleOrAdd(Expression<Func<T, bool>> ex, T obj, out T existing);
        TR FindSingleAs<TR>(Expression<Func<T, TR>> exp, object id) where TR : class;
        TR FindSingleAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> expression) where TR : class;

        void Add(T obj);

        void Update(T obj);
        void Delete(Expression<Func<T, bool>> ex);
        void Delete(T obj);
        void DeleteById(object id);
        void Merge(T obj);
        T Merge(Expression<Func<T, bool>> ex, T obj);

        List<T> GetList();
        List<T> Find(Expression<Func<T, bool>> exp);
        LoadResult<T> Find(ListOptions<T> opts);

        List<TR> FindAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> cond = null, ListOptions<TR> opts = null) where TR : class;
        LoadResult<TR> FindAs<TR>(Expression<Func<T, TR>> exp, ListOptions<TR> opts, Expression<Func<T, bool>> cond = null) where TR : class;
        List<TR> FindAs<TR>(Expression<Func<T, TR>> exp, IEnumerable<Expression<Func<T, bool>>> filtes) where TR : class;
        LoadResult FindAsSorted<TR, TV>(Expression<Func<T, TR>> exp, Expression<Func<T, TV>> sort, SortDir dir, ListOptions<TR> opts) where TR : class;
        int Count(Expression<Func<T, bool>> exp);
        TVal GetMax<TVal>(Expression<Func<T, TVal>> exp, Expression<Func<T, bool>> filter = null);
       
        bool Exist(Expression<Func<T, bool>> exp);
        bool IdExists(object id);
        DeleteResult CanDelete(object id);

    }
}
