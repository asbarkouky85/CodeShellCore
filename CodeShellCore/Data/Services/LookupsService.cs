using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;

using CodeShellCore.Data;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Services;
using CodeShellCore.Services.Http;
using CodeShellCore.Text;
using CodeShellCore.Data.ConfiguredCollections;


namespace CodeShellCore.Data.Services
{
    public class LookupsService<T> : ServiceBase where T : class, IUnitOfWork
    {
        protected T Unit;
        public LookupsService(T unit)
        {
            Unit = unit;
            Unit.EnableJsonLoading();
        }

        //public virtual object GetExternal(ExternalLookupQuery query) { return new { }; }

        public IEnumerable<Named<long>> GetLookupNamed<TObject>(string identifier) where TObject : class
        {
            IRepository<TObject> repo = Unit.GetRepositoryFor<TObject>();
            if (!(repo is INameableRepository<TObject>))
                throw new CodeShellHttpException(HttpStatusCode.ServiceUnavailable, $"Repository {repo.GetType().Name} must implement INameableRepositroy<{typeof(T).Name}> to be used for lookups");

            var namedRepo = (INameableRepository<TObject>)repo;
            string collectionId = null;
            if (identifier.Contains("__"))
            {
                collectionId = identifier.GetAfterLast("__");
            }
            return namedRepo.FindAsLookup(collectionId);
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
            if (identifier.Contains("__"))
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

    }
}
