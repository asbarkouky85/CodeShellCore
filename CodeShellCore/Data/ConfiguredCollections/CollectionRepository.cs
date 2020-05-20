using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Data.ConfiguredCollections
{
    public class CollectionRepository<T, TContext, TPrime> : KeyRepository<T, TContext, TPrime>, ICollectionEFRepository<T,TContext>
        where T : class, IModel<TPrime>
        where TContext : DbContext

    {
        protected readonly ICollectionConfigService _service;
        public CollectionRepository(TContext con, ICollectionConfigService service) : base(con)
        {
            _service = service;
        }

        protected IQueryable<T> QueryCollection(string collectionId)
        {
            var exp = _service.GetCollectionExpression<T>(collectionId);
            if (exp == null)
                throw new Exception("Unregistered collection " + collectionId);
            return Loader.Where(exp);
        }

        public IEnumerable<T> GetCollectionList(string collectionId)
        {
            return QueryCollection(collectionId).ToList();
        }

        public IEnumerable<TObject> GetCollectionListAs<TObject>(string collectionId, Expression<Func<T, TObject>> exp)
        {
            return QueryCollection(collectionId).Select(exp).ToList();
        }

        public LoadResult<T> LoadCollection(string collectionId, ListOptions<T> opts)
        {
            return QueryCollection(collectionId).LoadWith(opts);
        }

        public LoadResult<TObject> LoadCollectionAs<TObject>(string collectionId, Expression<Func<T, TObject>> exp, ListOptions<TObject> opts) where TObject : class
        {

            return QueryCollection(collectionId).Select(exp).LoadWith(opts);
        }
    }
}
