using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Extensions.DependencyInjection;
using CodeShellCore.Linq;
using CodeShellCore.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public virtual async Task<IEnumerable<T>> GetCollectionList(string collectionId)
        {
            return await QueryCollection(collectionId).ToListAsync();
        }

        public virtual async Task<IEnumerable<TObject>> GetCollectionListAs<TObject>(string collectionId, Expression<Func<T, TObject>> exp)
        {
            return await QueryCollection(collectionId).Select(exp).ToListAsync();
        }

        public virtual async Task<PagedResult<T>> LoadCollection(string collectionId, PagedListRequest<T> opts)
        {
            return await QueryCollection(collectionId).ToPagedResultAsync(opts);
        }

        public virtual async Task<PagedResult<TObject>> LoadCollectionAs<TObject>(string collectionId, Expression<Func<T, TObject>> exp, PagedListRequest<TObject> opts) where TObject : class
        {

            return await QueryCollection(collectionId).Select(exp).ToPagedResultAsync(opts);
        }

        public override async Task<IEnumerable<Named<object>>> FindAsLookup(string collectionId = null)
        {
            var l = collectionId == null ? Loader : QueryCollection(collectionId);
            return await QueryNamed(l).OrderBy(d => d.Name).ToListAsync();
        }

        public override async Task<IEnumerable<Named<object>>> FindAsLookup(string collectionId, Expression<Func<T, bool>> ex)
        {
            var l = collectionId == null ? Loader : QueryCollection(collectionId);
            l = l.Where(ex);
            return await QueryNamed(l).OrderBy(d => d.Name).ToListAsync();
        }

        public async Task<IEnumerable<TObject>> GetCollectionListAndMap<TObject>(string collectionId)
        {
            var q = QueryCollection(collectionId);
            return await QueryDto<TObject>(q).ToListAsync();
        }

        public async Task<PagedResult<TObject>> LoadCollectionAndMap<TObject>(string collectionId, PagedListRequest<TObject> opts) where TObject : class
        {
            var q = QueryCollection(collectionId);
            return await QueryDto<TObject>(q).ToPagedResultAsync(opts);
        }

        public override async Task<PagedResult<Named<object>>> FindAsLookupPaged(PagedListRequest request, string collectionId = null)
        {
            var req = request.GetOptionsFor<Named<object>>();
            var cQ = collectionId == null ? Loader : QueryCollection(collectionId);
            return await QueryNamed(cQ).ToPagedResultAsync(req);
        }
    }
}
