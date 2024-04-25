using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Services;
using CodeShellCore.Http;
using CodeShellCore.Text;
using CodeShellCore.Data.ConfiguredCollections;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Data.Mapping;
using System.Linq;
using CodeShellCore.Linq;

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

        public Dictionary<string, IEnumerable<NamedDto<object>>> GetRequestedLookups(Dictionary<string, string> requested)
        {
            Dictionary<string, IEnumerable<NamedDto<object>>> res = new Dictionary<string, IEnumerable<NamedDto<object>>>();
            foreach (var x in requested)
            {
                res[x.Key] = GetListNamed(x.Key, x.Value);
            }

            return res;
        }

        public virtual IEnumerable<NamedDto<object>> GetListNamed(string entityName, string collection = null)
        {
            var t = GetEntityByResource(entityName);
            if (t == null)
                return new List<NamedDto<object>>();
            return GetLookupNamed(t, collection);
        }

        public PagedResult<NamedDto<object>> GetListNamedPaged(string entityName, PagedListRequestDto dto, string identifier = null)
        {
            var t = GetEntityByResource(entityName);
            if (t == null)
                return new PagedResult<NamedDto<object>>();

            return GetLookupNamedPaged(t, dto, identifier);
        }

        public IEnumerable<NamedDto<object>> GetLookupNamed(Type t, string identifier)
        {
            IRepository repo = Unit.GetRepositoryFor(t);

            string collectionId = null;
            if (identifier != null && identifier.Contains("__"))
            {
                collectionId = identifier.GetAfterLast("__");
            }
            var data = repo.FindAsLookup(collectionId);
            return Mapper.Map(data, new List<NamedDto<object>>());
        }

        public PagedResult<NamedDto<object>> GetLookupNamedPaged(Type t, PagedListRequestDto req, string identifier)
        {
            IRepository repo = Unit.GetRepositoryFor(t);

            string collectionId = null;
            if (identifier != null && identifier.Contains("__"))
            {
                collectionId = identifier.GetAfterLast("__");
            }
            var data = repo.FindAsLookupPaged(Mapper.Map(req, new PagedListRequest()), collectionId);
            return Mapper.Map(data, new PagedResult<NamedDto<object>>());
        }

        public IEnumerable<NamedDto<object>> GetLookupNamed<TObject>(string identifier) where TObject : class
        {
            var data = GetLookupNamed(typeof(TObject), identifier);
            return Mapper.Map(data, new List<NamedDto<object>>());
        }

        public IEnumerable<TObject> GetLookup<TObject>(string identifier) where TObject : class
        {
            IRepository<TObject> repo = Unit.GetRepositoryFor<TObject>();
            if (identifier.Contains("__"))
            {
                string collectionId = identifier.GetAfterLast("__");

                if (!(repo is ICollectionRepository<TObject>))
                    throw new CodeShellHttpException(HttpStatusCode.ServiceUnavailable, $"Repository {repo.GetType().Name} must implement ICollectionRepository<{typeof(T).Name}> to use collections");

                var AsgaRepo = (ICollectionRepository<TObject>)repo;

                return AsgaRepo.GetCollectionList(collectionId);
            }
            else
            {
                return repo.Find(e => true);
            }
        }


        public IEnumerable<TResult> GetLookupAs<TObject, TResult>(string identifier, Expression<Func<TObject, TResult>> ex) where TObject : class where TResult : class
        {
            IRepository<TObject> repo = Unit.GetRepositoryFor<TObject>();
            if (identifier != null && identifier.Contains("__"))
            {
                string collectionId = identifier.GetAfterLast("__");

                if (!(repo is ICollectionRepository<TObject>))
                    throw new CodeShellHttpException(HttpStatusCode.ServiceUnavailable, $"Repository {repo.GetType().Name} must implement ICollectionRepository<{typeof(T).Name}> to use collections");

                var AsgaRepo = (ICollectionRepository<TObject>)repo;

                return AsgaRepo.GetCollectionListAs(collectionId, ex);
            }
            else
            {
                return repo.FindAs(ex, d => true);
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

        public IEnumerable<NamedDto<object>> GetLookupNamed<TObject>(string identifier, Expression<Func<TObject, bool>> ex) where TObject : class
        {
            IRepository<TObject> repo = Unit.GetRepositoryFor<TObject>();

            string collectionId = null;
            if (identifier != null && identifier.Contains("__"))
            {
                collectionId = identifier.GetAfterLast("__");
            }
            var data = repo.FindAsLookup(collectionId, ex);
            return Mapper.Map(data, new List<NamedDto<object>>());
        }


    }
}
