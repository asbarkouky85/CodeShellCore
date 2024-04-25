using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CodeShellCore.Data.ConfiguredCollections
{
    public class CollectionRepository<T, TContext, TPrime> : KeyRepository<T, TContext, TPrime>, ICollectionEFRepository<T, TContext>
        where T : class, IEntity<TPrime>
        where TContext : DbContext

    {
        protected readonly ICollectionConfigService _service;
        protected readonly IUserAccessor UserAccessor;

        public string CollectionId { get; set; }
        public string EntityName { get => typeof(T).Name; }

        public CollectionRepository(TContext con, ICollectionConfigService service, IUserAccessor acc) : base(con)
        {
            _service = service;
            this.UserAccessor = acc;
        }

        protected override IQueryable<T> GetLoader()
        {
            if (CollectionId != null)
            {
                var exp = _service.GetCollectionExpression<T>(CollectionId, UserAccessor);
                return base.GetLoader().Where(exp);
            }
            return base.GetLoader();

        }

        protected virtual IQueryable<T> QueryCollection(string collectionId)
        {
            var exp = _service.GetCollectionExpression<T>(collectionId, UserAccessor);
            if (exp == null)
                throw new Exception("Unregistered collection " + collectionId);
            return Loader.Where(exp);
        }

        public virtual IEnumerable<T> GetCollectionList(string collectionId)
        {
            return QueryCollection(collectionId).ToList();
        }

        public virtual IEnumerable<TObject> GetCollectionListAs<TObject>(string collectionId, Expression<Func<T, TObject>> exp)
        {
            return QueryCollection(collectionId).Select(exp).ToList();
        }

        public virtual PagedResult<T> LoadCollection(string collectionId, PagedListRequest<T> opts)
        {
            return QueryCollection(collectionId).ToPagedResult(opts);
        }

        public virtual PagedResult<TObject> LoadCollectionAs<TObject>(string collectionId, Expression<Func<T, TObject>> exp, PagedListRequest<TObject> opts) where TObject : class
        {

            return QueryCollection(collectionId).Select(exp).ToPagedResult(opts);
        }

        public override IEnumerable<Named<object>> FindAsLookup(string collectionId = null)
        {
            var l = collectionId == null ? Loader : QueryCollection(collectionId);
            return QueryNamed(l).OrderBy(d => d.Name).ToList();
        }

        public override IEnumerable<Named<object>> FindAsLookup(string collectionId, Expression<Func<T, bool>> ex)
        {
            var l = collectionId == null ? Loader : QueryCollection(collectionId);
            l = l.Where(ex);
            return QueryNamed(l).OrderBy(d => d.Name).ToList();
        }

        public IEnumerable<TObject> GetCollectionListAndMap<TObject>(string collectionId)
        {
            var q = QueryCollection(collectionId);
            return QueryDto<TObject>(q).ToList();
        }

        public PagedResult<TObject> LoadCollectionAndMap<TObject>(string collectionId, PagedListRequest<TObject> opts) where TObject : class
        {
            var q = QueryCollection(collectionId);
            return QueryDto<TObject>(q).ToPagedResult(opts);
        }

        public override PagedResult<Named<object>> FindAsLookupPaged(PagedListRequest request, string collectionId = null)
        {
            var req = request.GetOptionsFor<Named<object>>();
            var cQ = collectionId == null ? Loader : QueryCollection(collectionId);
            return QueryNamed(cQ).ToPagedResult(req);
        }
    }
}
