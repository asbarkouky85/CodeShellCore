using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Http;
using CodeShellCore.Linq;
using CodeShellCore.Services;
using CodeShellCore.Text;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeShellCore.Data.Lookups
{
    public abstract class LookupsService<T> : ServiceBase, ILookupsService
        where T : class, IUnitOfWork
    {
        protected abstract string EntitiesAssembly { get; }
        protected T Unit;
        protected readonly IObjectMapper Mapper;
        public LookupsService(T unit)
        {
            Unit = unit;
            Mapper = unit.ServiceProvider.GetRequiredService<IObjectMapper>();
        }

        protected virtual Dictionary<string, Type> ResourceToModel { get; }
        static Dictionary<string, Type> _resourceDictionary;

        protected virtual Type GetEntityByResource(string res)
        {
            var name = res.UCFirst();
            if (_resourceDictionary == null)
            {
                _resourceDictionary = ResourceToModel ?? new Dictionary<string, Type>();
            }
            if (_resourceDictionary.TryGetValue(name, out Type t))
            {
                return t;
            }
            else
            {
                var ent = name.Singularize();
                var tt = Assembly.Load(EntitiesAssembly).GetTypes();
                return tt.FirstOrDefault(e => e.Name == name.Singularize());
            }
        }

        public async Task<Dictionary<string, IEnumerable<Named<object>>>> GetRequestedLookups(Dictionary<string, string> requested)
        {
            Dictionary<string, IEnumerable<Named<object>>> res = new Dictionary<string, IEnumerable<Named<object>>>();
            foreach (var x in requested)
            {
                res[x.Key] = await GetListNamed(x.Key, x.Value);
            }

            return res;
        }

        public virtual async Task<IEnumerable<Named<object>>> GetListNamed(string entityName, string collection = null)
        {
            var t = GetEntityByResource(entityName);
            if (t == null)
                return new List<Named<object>>();
            return await GetLookupNamed(t, collection);
        }

        public async Task<PagedResult<Named<object>>> GetListNamedPaged(string entityName, PagedListRequestDto dto, string identifier = null)
        {
            var t = GetEntityByResource(entityName);
            if (t == null)
                return new PagedResult<Named<object>>();

            return await GetLookupNamedPaged(t, dto, identifier);
        }

        public async Task<IEnumerable<Named<object>>> GetLookupNamed(Type t, string identifier)
        {
            IRepository repo = Unit.GetRepositoryFor(t);

            string collectionId = null;
            if (identifier != null && identifier.Contains("__"))
            {
                collectionId = identifier.GetAfterLast("__");
            }
            var data = await repo.FindAsLookup(collectionId);
            return Mapper.Map(data, new List<Named<object>>());
        }

        public async Task<PagedResult<Named<object>>> GetLookupNamedPaged(Type t, PagedListRequestDto req, string identifier)
        {
            IRepository repo = Unit.GetRepositoryFor(t);

            string collectionId = null;
            if (identifier != null && identifier.Contains("__"))
            {
                collectionId = identifier.GetAfterLast("__");
            }
            var data = await repo.FindAsLookupPaged(Mapper.Map(req, new PagedListRequest()), collectionId);
            return Mapper.Map(data, new PagedResult<Named<object>>());
        }

        public async Task<IEnumerable<Named<object>>> GetLookupNamed<TObject>(string identifier) where TObject : class
        {
            var data = await GetLookupNamed(typeof(TObject), identifier);
            return Mapper.Map(data, new List<Named<object>>());
        }

        public async Task<IEnumerable<TObject>> GetLookup<TObject>(string identifier) where TObject : class
        {
            IRepository<TObject> repo = Unit.GetRepositoryFor<TObject>();
            if (identifier.Contains("__"))
            {
                string collectionId = identifier.GetAfterLast("__");

                if (!(repo is ICollectionRepository<TObject>))
                    throw new CodeShellHttpException(HttpStatusCode.ServiceUnavailable, $"Repository {repo.GetType().Name} must implement ICollectionRepository<{typeof(T).Name}> to use collections");

                var AsgaRepo = (ICollectionRepository<TObject>)repo;

                return await AsgaRepo.GetCollectionList(collectionId);
            }
            else
            {
                return await repo.Find(e => true);
            }
        }


        public async Task<IEnumerable<TResult>> GetLookupAs<TObject, TResult>(string identifier, Expression<Func<TObject, TResult>> ex) where TObject : class where TResult : class
        {
            IRepository<TObject> repo = Unit.GetRepositoryFor<TObject>();
            if (identifier != null && identifier.Contains("__"))
            {
                string collectionId = identifier.GetAfterLast("__");

                if (!(repo is ICollectionRepository<TObject>))
                    throw new CodeShellHttpException(HttpStatusCode.ServiceUnavailable, $"Repository {repo.GetType().Name} must implement ICollectionRepository<{typeof(T).Name}> to use collections");

                var AsgaRepo = (ICollectionRepository<TObject>)repo;

                return await AsgaRepo.GetCollectionListAs(collectionId, ex);
            }
            else
            {
                return await repo.FindAs(ex, d => true);
            }
        }

        protected string GetCollectionId(string identifier)
        {
            string collectionId = null;
            if (identifier.Contains("__"))
            {
                collectionId = identifier.GetAfterLast("__");
            }
            return collectionId;
        }

        public async Task<IEnumerable<Named<object>>> GetLookupNamed<TObject>(string identifier, Expression<Func<TObject, bool>> ex) where TObject : class
        {
            IRepository<TObject> repo = Unit.GetRepositoryFor<TObject>();

            string collectionId = null;
            if (identifier != null && identifier.Contains("__"))
            {
                collectionId = identifier.GetAfterLast("__");
            }
            var data = await repo.FindAsLookup(collectionId, ex);
            return Mapper.Map(data, new List<Named<object>>());
        }


    }
}
