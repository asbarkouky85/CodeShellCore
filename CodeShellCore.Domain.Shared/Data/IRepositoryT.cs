using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CodeShellCore.Data
{
    public interface IRepository<T> : IRepository where T : class
    {
        bool Exist(Expression<Func<T, bool>> exp);
        bool FindSingleOrAdd(Expression<Func<T, bool>> ex, T obj, out T existing);
        bool IdExists(object id);

        DeleteResult CanDelete(object id);
        IEnumerable<Named<object>> FindAsLookup(string collectionId, Expression<Func<T, bool>> ex);
        IEnumerable<TValue> GetValues<TValue, TOrder>(Expression<Func<T, TValue>> ex, Expression<Func<T, TOrder>> order, Expression<Func<T, bool>> filter = null);
        IEnumerable<TValue> GetValues<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter = null);
        int Count(Expression<Func<T, bool>> exp);
        List<T> Find(Expression<Func<T, bool>> exp);
        List<T> GetList();

        List<TR> FindAndMap<TR>(Expression<Func<T, bool>> cond = null, ListOptions<TR> opts = null) where TR : class;
        List<TR> FindAndMap<TR>(IEnumerable<Expression<Func<T, bool>>> filtes) where TR : class;
        LoadResult<TR> FindAndMap<TR>(ListOptions<TR> opts, Expression<Func<T, bool>> cond = null) where TR : class;

        TR FindSingleAndMap<TR>(Expression<Func<T, bool>> expression) where TR : class;
        TR FindSingleAndMap<TR>(object id) where TR : class;

        List<TR> FindAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> cond = null, ListOptions<TR> opts = null) where TR : class;
        List<TR> FindAs<TR>(Expression<Func<T, TR>> exp, IEnumerable<Expression<Func<T, bool>>> filtes) where TR : class;
        LoadResult<TR> FindAsSorted<TR, TV>(Expression<Func<T, TR>> exp, Expression<Func<T, TV>> sort, SortDir dir, ListOptions<TR> opts) where TR : class;
        LoadResult<T> Find(ListOptions<T> opts);
        LoadResult<TR> FindAs<TR>(Expression<Func<T, TR>> exp, ListOptions<TR> opts, Expression<Func<T, bool>> cond = null) where TR : class;
        T FindSingle(Expression<Func<T, bool>> expression);
        T FindSingle(object id);
        T Merge(Expression<Func<T, bool>> ex, T obj);
        TR FindSingleAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> expression) where TR : class;
        TR FindSingleAs<TR>(Expression<Func<T, TR>> exp, object id) where TR : class;
        TVal GetMax<TVal>(Expression<Func<T, TVal>> exp, Expression<Func<T, bool>> filter = null);
        TValue GetSingleValue<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter);
        TValue GetValue<TValue>(object id, Expression<Func<T, TValue>> ex);
        void Add(T obj);
        void Delete(Expression<Func<T, bool>> ex);
        void Delete(T obj);
        void DeleteById(object id);
        void Merge(T obj);
        void Update(T obj);
        //
        // Summary:
        //     Used to get a IQueryable that is used to retrieve entities from entire table.
        //     One or more
        //
        // Parameters:
        //   includes:
        //     A list of include expressions.
        //
        // Returns:
        //     IQueryable to be used to select entities from database
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includes);
    }
}
