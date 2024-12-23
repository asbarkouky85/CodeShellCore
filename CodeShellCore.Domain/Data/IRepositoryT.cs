using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeShellCore.Data
{
    public interface IRepository<T> : IRepository where T : class
    {
        bool FindSingleOrAdd(Expression<Func<T, bool>> ex, T obj, out T existing);

        Task<bool> Exist(Expression<Func<T, bool>> exp);
        Task<bool> IdExists(object id);

        Task<DeleteResult> CanDelete(object id);
        Task<IEnumerable<Named<object>>> FindAsLookup(string collectionId, Expression<Func<T, bool>> ex);
        Task<IEnumerable<TValue>> GetValues<TValue, TOrder>(Expression<Func<T, TValue>> ex, Expression<Func<T, TOrder>> order, Expression<Func<T, bool>> filter = null);
        Task<IEnumerable<TValue>> GetValues<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter = null);
        Task<int> Count(Expression<Func<T, bool>> exp);
        Task<List<T>> Find(Expression<Func<T, bool>> exp);
        Task<List<T>> GetList();

        Task<List<TR>> FindAndMap<TR>(Expression<Func<T, bool>> cond = null, PagedListRequest<TR> opts = null) where TR : class;
        Task<List<TR>> FindAndMapAsync<TR>(Expression<Func<T, bool>> cond = null, PagedListRequest<TR> opts = null) where TR : class;
        Task<List<TR>> FindAndMap<TR>(IEnumerable<Expression<Func<T, bool>>> filtes) where TR : class;
        Task<PagedResult<TR>> FindAndMap<TR>(PagedListRequest<TR> opts, Expression<Func<T, bool>> cond = null) where TR : class;

        Task<TR> FindSingleAndMap<TR>(Expression<Func<T, bool>> expression) where TR : class;
        Task<TR> FindSingleAndMap<TR>(object id) where TR : class;

        Task<List<TR>> FindAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> cond = null, PagedListRequest<TR> opts = null) where TR : class;
        Task<List<TR>> FindAs<TR>(Expression<Func<T, TR>> exp, IEnumerable<Expression<Func<T, bool>>> filtes) where TR : class;
        Task<PagedResult<TR>> FindAsSorted<TR, TV>(Expression<Func<T, TR>> exp, Expression<Func<T, TV>> sort, SortDir dir, PagedListRequest<TR> opts) where TR : class;
        Task<PagedResult<T>> Find(PagedListRequest<T> opts);
        Task<PagedResult<TR>> FindAs<TR>(Expression<Func<T, TR>> exp, PagedListRequest<TR> opts, Expression<Func<T, bool>> cond = null) where TR : class;
        Task<T> FindSingle(Expression<Func<T, bool>> expression);
        Task<T> FindSingle(object id);
        Task<T> Merge(Expression<Func<T, bool>> ex, T obj);
        Task MergeAsync(IEnumerable<T> list, Action<T, T> updateAction = null);
        Task<TR> FindSingleAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> expression) where TR : class;
        Task<TR> FindSingleAs<TR>(Expression<Func<T, TR>> exp, object id) where TR : class;
        Task<TVal> GetMax<TVal>(Expression<Func<T, TVal>> exp, Expression<Func<T, bool>> filter = null);
        Task<TValue> GetSingleValue<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter);
        Task<TValue> GetValue<TValue>(object id, Expression<Func<T, TValue>> ex);
        void Add(T obj);
        Task Delete(Expression<Func<T, bool>> ex);
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

        Task InsertAsync(T tmp);
        Task<T> GetAsync(Expression<Func<T, bool>> value);
        Task DeleteAsync(T tmpFile);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> value);
    }
}
