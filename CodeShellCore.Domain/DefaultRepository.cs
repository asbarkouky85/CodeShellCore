using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Linq;

namespace CodeShellCore.Data
{
    public class DefaultRepository<T> : IRepository<T> where T : class
    {
        static List<T> _storedData = new List<T>();

        public IQueryProjector Projector { get; set; }

        public void Add(T obj)
        {
            _storedData.Add(obj);
        }

        public Task<IEnumerable> All()
        {
            return Task.Run(() => (IEnumerable)_storedData);
        }

        public Task<DeleteResult> CanDelete(object id)
        {
            return Task.Run(() => new DeleteResult { CanDelete = true });
        }

        public Task<int> Count(Expression<Func<T, bool>> exp)
        {
            return Task.Run(() => _storedData.Count(exp.Compile()));
        }

        public Task<int> Count()
        {
            return Task.Run(() => _storedData.Count());
        }

        public Task Delete(Expression<Func<T, bool>> ex)
        {
            return Task.Run(() =>
            {
                var ifCond = ex.Compile();
                var newData = new List<T>();
                foreach (var i in _storedData)
                {
                    var x = ifCond.Invoke(i);
                    if (!x)
                        newData.Add(i);
                }
                _storedData = newData;
            });
        }

        public void Delete(T obj)
        {
            _storedData.Remove(obj);
        }

        public Task DeleteAsync(T tmpFile)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(object id)
        {

        }

        public Task<bool> Exist(Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> Find(Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<T>> Find(PagedListRequest<T> opts)
        {
            throw new NotImplementedException();
        }

        public Task<List<TR>> FindAndMap<TR>(Expression<Func<T, bool>> cond = null, PagedListRequest<TR> opts = null) where TR : class
        {
            throw new NotImplementedException();
        }

        public Task<List<TR>> FindAndMap<TR>(IEnumerable<Expression<Func<T, bool>>> filtes) where TR : class
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<TR>> FindAndMap<TR>(PagedListRequest<TR> opts, Expression<Func<T, bool>> cond = null) where TR : class
        {
            throw new NotImplementedException();
        }

        public Task<List<TR>> FindAndMapAsync<TR>(Expression<Func<T, bool>> cond = null, PagedListRequest<TR> opts = null) where TR : class
        {
            throw new NotImplementedException();
        }

        public Task<List<TR>> FindAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> cond = null, PagedListRequest<TR> opts = null) where TR : class
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<TR>> FindAs<TR>(Expression<Func<T, TR>> exp, PagedListRequest<TR> opts, Expression<Func<T, bool>> cond = null) where TR : class
        {
            throw new NotImplementedException();
        }

        public Task<List<TR>> FindAs<TR>(Expression<Func<T, TR>> exp, IEnumerable<Expression<Func<T, bool>>> filtes) where TR : class
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Named<object>>> FindAsLookup(string collectionId, Expression<Func<T, bool>> ex)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Named<object>>> FindAsLookup(string collectionId = null)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Named<object>>> FindAsLookupPaged(PagedListRequest request, string collectionId = null)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<TR>> FindAsSorted<TR, TV>(Expression<Func<T, TR>> exp, Expression<Func<T, TV>> sort, SortDir dir, PagedListRequest<TR> opts) where TR : class
        {
            throw new NotImplementedException();
        }

        public Task<T> FindSingle(object id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindSingle(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<TR> FindSingleAndMap<TR>(Expression<Func<T, bool>> expression) where TR : class
        {
            throw new NotImplementedException();
        }

        public Task<TR> FindSingleAndMap<TR>(object id) where TR : class
        {
            throw new NotImplementedException();
        }

        public Task<TR> FindSingleAs<TR>(Expression<Func<T, TR>> exp, object id) where TR : class
        {
            throw new NotImplementedException();
        }

        public Task<TR> FindSingleAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> expression) where TR : class
        {
            throw new NotImplementedException();
        }

        public bool FindSingleOrAdd(Expression<Func<T, bool>> ex, T obj, out T existing)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> value)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetListAsync(Expression<Func<T, bool>> value)
        {
            throw new NotImplementedException();
        }

        public Task<TVal> GetMax<TVal>(Expression<Func<T, TVal>> exp, Expression<Func<T, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<TValue> GetSingleValue<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<TValue> GetValue<TValue>(object id, Expression<Func<T, TValue>> ex)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TValue>> GetValues<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TValue>> GetValues<TValue, TOrder>(Expression<Func<T, TValue>> ex, Expression<Func<T, TOrder>> order, Expression<Func<T, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IdExists(object id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(T tmp)
        {
            throw new NotImplementedException();
        }

        public void Merge(T obj)
        {
            throw new NotImplementedException();
        }

        public Task<T> Merge(Expression<Func<T, bool>> ex, T obj)
        {
            throw new NotImplementedException();
        }

        public Task MergeAsync(IEnumerable<T> list, Action<T, T> updateAction = null)
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
