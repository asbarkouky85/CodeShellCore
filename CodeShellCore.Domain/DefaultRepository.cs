using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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

        public IEnumerable All()
        {
            return _storedData;
        }

        public DeleteResult CanDelete(object id)
        {
            return new DeleteResult { CanDelete = true };
        }

        public int Count(Expression<Func<T, bool>> exp)
        {
            return _storedData.Count(exp.Compile());
        }

        public int Count()
        {
            return _storedData.Count();
        }

        public void Delete(Expression<Func<T, bool>> ex)
        {
            var ifCond = ex.Compile();
            var newData = new List<T>();
            foreach(var i in _storedData)
            {
                var x = ifCond.Invoke(i);
                if (!x)
                    newData.Add(i);
            }
            _storedData = newData;
        }

        public void Delete(T obj)
        {
            _storedData.Remove(obj);
        }

        public void DeleteById(object id)
        {
           
        }

        public bool Exist(Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public List<T> Find(Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public LoadResult<T> Find(ListOptions<T> opts)
        {
            throw new NotImplementedException();
        }

        public List<TR> FindAndMap<TR>(Expression<Func<T, bool>> cond = null, ListOptions<TR> opts = null) where TR : class
        {
            throw new NotImplementedException();
        }

        public List<TR> FindAndMap<TR>(IEnumerable<Expression<Func<T, bool>>> filtes) where TR : class
        {
            throw new NotImplementedException();
        }

        public LoadResult<TR> FindAndMap<TR>(ListOptions<TR> opts, Expression<Func<T, bool>> cond = null) where TR : class
        {
            throw new NotImplementedException();
        }

        public List<TR> FindAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> cond = null, ListOptions<TR> opts = null) where TR : class
        {
            throw new NotImplementedException();
        }

        public LoadResult<TR> FindAs<TR>(Expression<Func<T, TR>> exp, ListOptions<TR> opts, Expression<Func<T, bool>> cond = null) where TR : class
        {
            throw new NotImplementedException();
        }

        public List<TR> FindAs<TR>(Expression<Func<T, TR>> exp, IEnumerable<Expression<Func<T, bool>>> filtes) where TR : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Named<object>> FindAsLookup(string collectionId, Expression<Func<T, bool>> ex)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Named<object>> FindAsLookup(string collectionId = null)
        {
            throw new NotImplementedException();
        }

        public LoadResult<TR> FindAsSorted<TR, TV>(Expression<Func<T, TR>> exp, Expression<Func<T, TV>> sort, SortDir dir, ListOptions<TR> opts) where TR : class
        {
            throw new NotImplementedException();
        }

        public T FindSingle(object id)
        {
            throw new NotImplementedException();
        }

        public T FindSingle(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public TR FindSingleAndMap<TR>(Expression<Func<T, bool>> expression) where TR : class
        {
            throw new NotImplementedException();
        }

        public TR FindSingleAndMap<TR>(object id) where TR : class
        {
            throw new NotImplementedException();
        }

        public TR FindSingleAs<TR>(Expression<Func<T, TR>> exp, object id) where TR : class
        {
            throw new NotImplementedException();
        }

        public TR FindSingleAs<TR>(Expression<Func<T, TR>> exp, Expression<Func<T, bool>> expression) where TR : class
        {
            throw new NotImplementedException();
        }

        public bool FindSingleOrAdd(Expression<Func<T, bool>> ex, T obj, out T existing)
        {
            throw new NotImplementedException();
        }

        public List<T> GetList()
        {
            throw new NotImplementedException();
        }

        public TVal GetMax<TVal>(Expression<Func<T, TVal>> exp, Expression<Func<T, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public TValue GetSingleValue<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public TValue GetValue<TValue>(object id, Expression<Func<T, TValue>> ex)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TValue> GetValues<TValue>(Expression<Func<T, TValue>> ex, Expression<Func<T, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TValue> GetValues<TValue, TOrder>(Expression<Func<T, TValue>> ex, Expression<Func<T, TOrder>> order, Expression<Func<T, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public bool IdExists(object id)
        {
            throw new NotImplementedException();
        }

        public void Merge(T obj)
        {
            throw new NotImplementedException();
        }

        public T Merge(Expression<Func<T, bool>> ex, T obj)
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
